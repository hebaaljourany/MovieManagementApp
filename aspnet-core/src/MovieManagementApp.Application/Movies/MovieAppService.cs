using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Minio;
using MovieManagementApp.Actors;
using MovieManagementApp.Application.Contracts.Movies;
using MovieManagementApp.Blob;
using MovieManagementApp.Categories;
using MovieManagementApp.MovieActors;
using MovieManagementApp.MovieCategories;
using MovieManagementApp.MyAccounts;
using MovieManagementApp.MyLists;
using MovieManagementApp.Permissions;
using MovieManagementApp.Ratings;
using MovieManagementApp.UserMovieInteractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Content;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace MovieManagementApp.Movies
{
    [RemoteService(IsEnabled = true)]
    public class MovieAppService : CrudAppService<
        Movie, // The Movie entity
        MovieDto, // Used to show movies
        Guid, // Primary key of the movie entity
        PagedAndSortedResultRequestDto, // Used for paging/sorting
        CreateUpdateMovieDto>, // Used to create/update a movie
        IMovieAppService
    {
        private readonly ICurrentUser _currentUser;
        private readonly IRepository<Rating, Guid> _ratingRepository;
        private readonly IRepository<UserMovieInteraction, Guid> _userMovieInteractionRepository;
        private readonly IRepository<MyAccount, Guid> _myAccountRepository;
        private readonly IRepository<MyList, Guid> _myListRepository;
        private readonly IRepository<Actor, Guid> _actorRepository;
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<MovieActor, Guid> _movieActorRepository;
        private readonly IRepository<MovieCategory, Guid> _movieCategoryRepository;
        private readonly ILogger<MovieAppService> _logger;
        protected HttpContext HttpContext => _httpContextAccessor.HttpContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBlobContainer<MovieContainer> _blobContainer;


        public MovieAppService(
            IRepository<Movie, Guid> repository,
            ICurrentUser currentUser,
            IRepository<Rating, Guid> ratingRepository,
            IRepository<UserMovieInteraction, Guid> userMovieInteractionRepository,
            IRepository<MyAccount, Guid> myAccountRepository,
            IRepository<MyList, Guid> myListRepository,
            IRepository<Actor, Guid> actorRepository,
            IRepository<Category, Guid> categoryRepository,
            IRepository<MovieActor, Guid> movieActorRepository,
            IRepository<MovieCategory, Guid> movieCategoryRepository,
            ILogger<MovieAppService> logger,
            IHttpContextAccessor httpContextAccessor
,
            IBlobContainer<MovieContainer> blobContainer



        ) : base(repository)
        {
            _currentUser = currentUser;
            _ratingRepository = ratingRepository;
            _userMovieInteractionRepository = userMovieInteractionRepository;
            _myAccountRepository = myAccountRepository;
            _myListRepository = myListRepository;
            _actorRepository = actorRepository;
            _categoryRepository = categoryRepository;
            _movieActorRepository = movieActorRepository;
            _movieCategoryRepository = movieCategoryRepository;
            _logger = logger;
            GetPolicyName = MovieManagementAppPermissions.Movies.Default;
            GetListPolicyName = MovieManagementAppPermissions.Movies.Default;
            CreatePolicyName = MovieManagementAppPermissions.Movies.Create;
            UpdatePolicyName = MovieManagementAppPermissions.Movies.Edit;
            DeletePolicyName = MovieManagementAppPermissions.Movies.Delete;
            _httpContextAccessor = httpContextAccessor;
            _blobContainer = blobContainer;
        }
        [HttpGet("stream-video")]
        public async Task<IRemoteStreamContent> StreamVideo(string blobName)
        {
            //var objectName = "[EgyBest].House.S06E21.BluRay.720p.x264.mp4 - cfd6664b-028f-4f25-a562-4dc3e0157616"; // The name of the video file in MinIO


            var stream = await _blobContainer.GetAllBytesAsync(blobName);
            var movieStream = new MemoryStream(stream);

            // Check for range header
            if (HttpContext.Request.Headers.ContainsKey("Range"))
            {
                var range = HttpContext.Request.Headers["Range"].ToString();
                var rangeHeader = RangeHeaderValue.Parse(range);
                var start = rangeHeader.Ranges.First().From ?? 0;
                var end = rangeHeader.Ranges.First().To ?? (stream.Length - 1);

                HttpContext.Response.StatusCode = StatusCodes.Status206PartialContent; // Partial content status
                HttpContext.Response.Headers.Add("Content-Range", $"bytes {start}-{end}/{stream.Length}");
                HttpContext.Response.Headers.Add("Accept-Ranges", "bytes");
                HttpContext.Response.ContentType = "video/mp4";

                // Create a stream for the specified byte range
                
                movieStream.Seek(start, SeekOrigin.Begin);

                return new RemoteStreamContent(movieStream, blobName, "video/mp4");
            }
            else
            {
                // If no range is specified, return the entire video
                return new RemoteStreamContent(movieStream, blobName, "video/mp4");
            }
        }
        //[HttpGet("stream-video")]

        //public async Task<IRemoteStreamContent> StreamVideo()
        //{
        //    var relativePath = "gg.mp4";
        //    var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        //    var fullPath = Path.Combine(webRootPath, relativePath);

        private async Task<Guid> GetMyAccountIdAsync()
        {
            if (!_currentUser.IsAuthenticated)
                throw new Exception("User must be authenticated");

            // Retrieve the current user's ID
            var userId = _currentUser.Id;

            // Query to get the myAccountId based on the current user
            var myAccountId = await (from myAccount in await _myAccountRepository.GetQueryableAsync()
                                     where myAccount.UserId == userId
                                     select myAccount.Id).FirstOrDefaultAsync();

            if (myAccountId == default)
                throw new Exception("Account not found for the current user");

            return myAccountId;
        }
        // Get movie details along with actors, categories, and rating

        public override async Task<MovieDto> GetAsync(Guid id)
        {

            // Get the IQueryable<Movie> from the repository
            var queryable = await Repository.GetQueryableAsync();

            // تأكد من أن هناك فيلمًا بهذا المعرّف
            var movie = await AsyncExecuter.FirstOrDefaultAsync(queryable.Where(m => m.Id == id));
            if (movie == null)
            {
                throw new EntityNotFoundException(typeof(Movie), id);
            }

            // Get actors associated with the movie
            var movieActorsQuery = from movieActor in await _movieActorRepository.GetQueryableAsync()
                                   join actor in await _actorRepository.GetQueryableAsync() on movieActor.ActorId equals actor.Id
                                   where movieActor.MovieId == id
                                   select new ActorDto
                                   {
                                       Id = actor.Id,
                                       ActorName = actor.ActorName
                                   };

            var actors = await AsyncExecuter.ToListAsync(movieActorsQuery);

            // Get categories associated with the movie
            var movieCategoriesQuery = from movieCategory in await _movieCategoryRepository.GetQueryableAsync()
                                       join category in await _categoryRepository.GetQueryableAsync() on movieCategory.CategoryId equals category.Id
                                       where movieCategory.MovieId == id
                                       select new CategoryDto
                                       {
                                           Id = category.Id,
                                           CategoryName = category.CategoryName
                                       };

            var categories = await AsyncExecuter.ToListAsync(movieCategoriesQuery);

                // Map movie to DTO
                var movieDto = ObjectMapper.Map<Movie, MovieDto>(movie);
                movieDto.Actors = actors;
                movieDto.Categories = categories;
                var posterBlob = await _blobContainer.GetAllBytesAsync(movie.PosterBlob);
                movieDto.PosterBlob = Convert.ToBase64String(posterBlob);

            // Include average rating if needed
            movieDto.AverageRating = await CalculateAverageRatingAsync(id);

            return movieDto;

        }

        public override async Task<PagedResultDto<MovieDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            // 1. Get the base query for movies with pagination and sorting
            var queryable = await Repository.GetQueryableAsync();

            // 2. Apply sorting and pagination
            var moviesQuery = queryable
              // .OrderBy(input.Sorting ?? nameof(Movie.Title))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            // 3. Fetch the list of movies
            var movies = await AsyncExecuter.ToListAsync(moviesQuery);

            // 4. Convert movies to MovieDto and populate each with related Actors and Categories
            var movieDtos = new List<MovieDto>();

            foreach (var movie in movies)
            {
                // Map the movie to DTO
                var movieDto = ObjectMapper.Map<Movie, MovieDto>(movie);

                // Get actors associated with the movie
                var movieActorsQuery = from movieActor in await _movieActorRepository.GetQueryableAsync()
                                       join actor in await _actorRepository.GetQueryableAsync() on movieActor.ActorId equals actor.Id
                                       where movieActor.MovieId == movie.Id
                                       select new ActorDto
                                       {
                                           Id = actor.Id,
                                           ActorName = actor.ActorName
                                       };
                movieDto.Actors = await AsyncExecuter.ToListAsync(movieActorsQuery);

                // Get categories associated with the movie
                var movieCategoriesQuery = from movieCategory in await _movieCategoryRepository.GetQueryableAsync()
                                           join category in await _categoryRepository.GetQueryableAsync() on movieCategory.CategoryId equals category.Id
                                           where movieCategory.MovieId == movie.Id
                                           select new CategoryDto
                                           {
                                               Id = category.Id,
                                               CategoryName = category.CategoryName
                                           };
                movieDto.Categories = await AsyncExecuter.ToListAsync(movieCategoriesQuery);

                // Add the completed movie DTO to the list
                movieDtos.Add(movieDto);
            }

            // 5. Count the total number of movies for paging
            var totalCount = await AsyncExecuter.CountAsync(queryable);

            // 6. Return the paginated result
            return new PagedResultDto<MovieDto>(totalCount, movieDtos);
        }


        // Create Movie - with default values for certain properties
        public override async Task<MovieDto> CreateAsync([FromForm] CreateUpdateMovieDto input)
         {
           
             var movie = ObjectMapper.Map<CreateUpdateMovieDto, Movie>(input);


            movie.AverageRating = 0;
            movie.TotalViews = 0;
            movie.TotalDownloads = 0;

            var movieBlobName = await UploadFileAsync(input.Blob);
            var posterBlobName = await UploadFileAsync(input.PosterBlob);
            movie.MovieBlob = movieBlobName;
            movie.PosterBlob = posterBlobName;
            

            try
            {

                if (input.ActorIds is not null && input.ActorIds.Any())
                {
                    var acts = new List<MovieActor>();
                    foreach (var actorId in input.ActorIds)
                    {
                        acts.Add(new MovieActor
                        {
                            ActorId = actorId
                        });
                    }
                    movie.MovieActors = acts;
                }


                if (input.CategoryIds is not null && input.CategoryIds.Any())
                {
                    var cats = new List<MovieCategory>();
                    foreach (var categoryId in input.CategoryIds)
                    {
                        cats.Add(new MovieCategory
                        {
                            CategoryId = categoryId
                        });
                    }
                    movie.MovieCategories = cats;
                }

                movie = await Repository.InsertAsync(movie, true);


                return ObjectMapper.Map<Movie, MovieDto>(movie);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the movie relationships. The movie has been deleted.", ex);
            }
        }
        // Update Movie
        public override async Task<MovieDto> UpdateAsync(Guid id, CreateUpdateMovieDto input)
        {
            // التحقق من أن الإدخال غير فارغ
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");
            }

            // 1. استرجاع الفيلم الحالي
            var movie = await Repository.GetAsync(id);

            // 2. تحديث خصائص الفيلم الأساسية
            movie.Title = input.Title;
            movie.Duration = input.Duration;
            movie.Description = input.Description;
            movie.AgeRating = input.AgeRating;
            movie.ReleaseDate = input.ReleaseDate;

            // 3. حذف العلاقات القديمة مع الممثلين
            var existingMovieActors = await _movieActorRepository.GetListAsync(x => x.MovieId == movie.Id);
            foreach (var actor in existingMovieActors)
            {
                await _movieActorRepository.DeleteAsync(actor.Id);
            }

            // 4. تحديث العلاقات الجديدة مع الممثلين
            if (input.ActorIds != null)
            {
                movie.MovieActors = input.ActorIds.Select(actorId => new MovieActor { ActorId = actorId, MovieId = movie.Id }).ToList();
            }
            else
            {
                movie.MovieActors = new List<MovieActor>();
            }

            // 5. حذف العلاقات القديمة مع التصنيفات
            var existingMovieCategories = await _movieCategoryRepository.GetListAsync(x => x.MovieId == movie.Id);
            foreach (var category in existingMovieCategories)
            {
                await _movieCategoryRepository.DeleteAsync(category.Id);
            }

            // 6. تحديث العلاقات الجديدة مع التصنيفات
            if (input.CategoryIds != null)
            {
                movie.MovieCategories = input.CategoryIds.Select(categoryId => new MovieCategory { CategoryId = categoryId, MovieId = movie.Id }).ToList();
            }
            else
            {
                movie.MovieCategories = new List<MovieCategory>();
            }

            // 7. حفظ التحديثات في قاعدة البيانات
            var updatedMovie = await Repository.UpdateAsync(movie);

            // 8. إرجاع الكائن المحدث
            return ObjectMapper.Map<Movie, MovieDto>(updatedMovie);
        }

        // Method to update MovieActors
        private void UpdateMovieActors(Movie movie, List<Guid> actorIds)
        {
            var currentActorIds = movie.MovieActors.Select(ma => ma.ActorId).ToHashSet();

            // Remove any actors that are no longer in the updated list
            movie.MovieActors.RemoveAll(ma => !actorIds.Contains(ma.ActorId));

            // Add new actors that are not already associated with the movie
            foreach (var actorId in actorIds)
            {
                if (!currentActorIds.Contains(actorId))
                {
                    movie.MovieActors.Add(new MovieActor { ActorId = actorId });
                }
            }
        }

        // Method to update MovieCategories
        private void UpdateMovieCategories(Movie movie, List<Guid> categoryIds)
        {
            var currentCategoryIds = movie.MovieCategories.Select(mc => mc.CategoryId).ToHashSet();

            // Remove any categories that are no longer in the updated list
            movie.MovieCategories.RemoveAll(mc => !categoryIds.Contains(mc.CategoryId));

            // Add new categories that are not already associated with the movie
            foreach (var categoryId in categoryIds)
            {
                if (!currentCategoryIds.Contains(categoryId))
                {
                    movie.MovieCategories.Add(new MovieCategory { CategoryId = categoryId });
                }
            }
        }

        /*public override async Task<MovieDto> UpdateAsync(Guid id, CreateUpdateMovieDto input)
        {
            // Check input for null
            if (input == null) throw new ArgumentNullException(nameof(input), "Input cannot be null.");
            // 1. Retrieve the current movie
            var movie = await Repository.GetAsync(id);

            // 2. Update basic movie properties
            movie.Title = input.Title;
            movie.Duration = input.Duration;
            movie.Description = input.Description;
            movie.AgeRating = input.AgeRating;
            movie.ReleaseDate = input.ReleaseDate;
            movie.PosterUrl = input.PosterUrl;
            movie.CoverUrl = input.CoverUrl;
            movie.VideoUrl = input.VideoUrl;

            // 3. Update actor relations
            if (input.ActorIds is not null && input.ActorIds.Any())
            {
                var acts = new List<MovieActor>();
                foreach (var actorId in input.ActorIds)
                {
                    acts.Add(new MovieActor
                    {
                        ActorId = actorId
                    });
                }
                movie.MovieActors = movie.MovieActors ?? new List<MovieActor>();
                movie.MovieActors.Clear();
                movie.MovieActors = acts;
            }
            


            // 4. Update category relations
            if (input.CategoryIds is not null && input.CategoryIds.Any())
            {
                var cats = new List<MovieCategory>();
                foreach (var categoryId in input.CategoryIds)
                {
                    cats.Add(new MovieCategory
                    {
                        CategoryId = categoryId
                    });
                }
                movie.MovieCategories = movie.MovieCategories ?? new List<MovieCategory>();
                movie.MovieCategories.Clear();
                movie.MovieCategories = cats;
            }




            // 5. Save updates to the database
            movie = await Repository.UpdateAsync(movie);

            // 6. Return the updated MovieDto
            return ObjectMapper.Map<Movie, MovieDto>(movie);
        }*/

        /*     public override async Task<MovieDto> UpdateAsync(Guid id, CreateUpdateMovieDto input)
             {
                 // 1. إحضار الفيلم الحالي
                 var movie = await Repository.GetAsync(id);

                 if (movie == null)
                 {
                     throw new Exception($"Movie with ID {id} not found.");
                 }

            // 2. تحديث بيانات الفيلم الأساسية
            movie.Title = input.Title;
            movie.Duration = input.Duration;
            movie.Description = input.Description;
            movie.AgeRating = input.AgeRating;
            movie.ReleaseDate = input.ReleaseDate;

                 // 3. تحديث العلاقات الخاصة بالممثلين
                 if (input.ActorIds != null)
                 {
                     // إزالة الممثلين الحاليين
                     movie.MovieActors.Clear();

                     // إضافة الممثلين الجدد
                     foreach (var actorId in input.ActorIds)
                     {
                         movie.MovieActors.Add(new MovieActor
                         {
                             MovieId = movie.Id,
                             ActorId = actorId
                         });
                     }
                 }

                 // 4. تحديث العلاقات الخاصة بالتصنيفات
                 if (input.CategoryIds != null)
                 {
                     // إزالة التصنيفات الحالية
                     movie.MovieCategories.Clear();

                     // إضافة التصنيفات الجديدة
                     foreach (var categoryId in input.CategoryIds)
                     {
                         movie.MovieCategories.Add(new MovieCategory
                         {
                             MovieId = movie.Id,
                             CategoryId = categoryId
                         });
                     }
                 }

                 // 5. حفظ التحديثات في قاعدة البيانات
                 await Repository.UpdateAsync(movie);

                 // 6. إرجاع MovieDto المحدثة
                 return ObjectMapper.Map<Movie, MovieDto>(movie);
             }*/
        // Delete Movie
        public override async Task DeleteAsync(Guid id)
        {
            // تحقق من وجود الفيلم
            var movie = await Repository.GetAsync(id);
            if (movie == null)
            {
                throw new EntityNotFoundException(typeof(Movie), id);
            }

            // حذف العلاقات مع التصنيفات
            var movieCategories = await _movieCategoryRepository.GetListAsync(m => m.MovieId == id);
            foreach (var movieCategory in movieCategories)
            {
                await _movieCategoryRepository.DeleteAsync(movieCategory);
            }

            // حذف العلاقات مع الممثلين
            var movieActors = await _movieActorRepository.GetListAsync(m => m.MovieId == id);
            foreach (var movieActor in movieActors)
            {
                await _movieActorRepository.DeleteAsync(movieActor);
            }

            // حذف التقييمات المرتبطة بالفيلم
            var ratings = await _ratingRepository.GetListAsync(r => r.MovieId == id);
            foreach (var rating in ratings)
            {
                await _ratingRepository.DeleteAsync(rating);
            }

            // حذف التفاعلات الخاصة بالفيلم
            var userMovieInteractions = await _userMovieInteractionRepository.GetListAsync(umi => umi.MovieId == id);
            foreach (var interaction in userMovieInteractions)
            {
                await _userMovieInteractionRepository.DeleteAsync(interaction);
            }

            // أخيرًا، حذف الفيلم
            await Repository.DeleteAsync(movie);

            _logger.LogInformation($"Movie with ID {id} has been deleted successfully.");
        }


        // Rate a movie
        public async Task RateMovieAsync(Guid movieId, int ratingValue)
        {

            var myAccountId = await GetMyAccountIdAsync();
            var ratable = await IsWatchedOrDownloadedAsync(movieId);
            if (ratable)
            {
                var movie = await Repository.GetAsync(movieId);
                var existingRating = await _ratingRepository.FirstOrDefaultAsync(r => r.MovieId == movieId && r.MyAccountId == myAccountId);

                if (existingRating != null)
                {
                    existingRating.RatingValue = ratingValue;
                    await _ratingRepository.UpdateAsync(existingRating);
                }
                else
                {
                    var rating = new Rating
                    {
                        MovieId = movieId,
                        MyAccountId = myAccountId,
                        RatingValue = ratingValue
                    };
                    await _ratingRepository.InsertAsync(rating);
                }

                // Update average rating
                movie.AverageRating = await CalculateAverageRatingAsync(movieId);
                await Repository.UpdateAsync(movie);
            }
            else
            {
                throw new Exception("Movie must be watched or downloaded");
            }
        }

        // Get the current user's rating for a specific movie, or return 0 if no rating exists
        public async Task<int> GetUserRatingAsync(Guid movieId)
        {
            var myAccountId = await GetMyAccountIdAsync();  // الحصول على myAccountId الخاص بالمستخدم الحالي

            // ابحث عن تقييم المستخدم الحالي للفيلم المحدد
            var existingRating = await _ratingRepository.FirstOrDefaultAsync(r => r.MovieId == movieId && r.MyAccountId == myAccountId);

            // إذا كان هناك تقييم، قم بإرجاعه، وإذا لم يوجد تقييم، قم بإرجاع 0
            return existingRating != null ? existingRating.RatingValue : 0;
        }

        // Add movie to the user's list
        public async Task AddToMyListAsync(Guid movieId)
        {
            var myAccountId = await GetMyAccountIdAsync();

            var exists = await IsInMyListAsync(movieId);
            if (!exists)
            {
                var myList = new MyList
                {
                    MovieId = movieId,
                    MyAccountId = myAccountId,

                };
                await _myListRepository.InsertAsync(myList);
            }
            else
            {
                throw new Exception("This movie is already in your list");
            }
        }

        // Check if movie is in user's list
        public async Task<bool> IsInMyListAsync(Guid movieId)
        {
            var myAccountId = await GetMyAccountIdAsync();

            return await _myListRepository.AnyAsync(umi => umi.MovieId == movieId && umi.MyAccountId == myAccountId);
        }
        public async Task<bool> IsWatchedOrDownloadedAsync(Guid movieId)
        {
            var myAccountId = await GetMyAccountIdAsync();

            return await _userMovieInteractionRepository.AnyAsync(umi => umi.MovieId == movieId && umi.MyAccountId == myAccountId);
        }
        // Calculate average rating
        public async Task<float> CalculateAverageRatingAsync(Guid movieId)
        {
            var ratings = await _ratingRepository.GetListAsync(r => r.MovieId == movieId);
            if (!ratings.Any()) return 0;
            return (float)ratings.Average(r => r.RatingValue);
        }

        // Calculate total views
        public async Task<int> GetTotalViewsAsync(Guid movieId, DateTime? from = null, DateTime? to = null)
        {
            var query = await _userMovieInteractionRepository.GetQueryableAsync();

            // إذا كانت تواريخ البداية والنهاية محددة
            if (from.HasValue && to.HasValue)
            {
                return query.Count(umi => umi.MovieId == movieId
                                          && umi.Interaction == InteractionType.Watched
                                          && umi.CreationTime >= from.Value
                                          && umi.CreationTime <= to.Value);
            }

            // إذا لم يتم تحديد نطاق زمني
            return query.Count(umi => umi.MovieId == movieId
                                      && umi.Interaction == InteractionType.Watched);
        }

        // Calculate total downloads
        public async Task<int> GetTotalDownloadsAsync(Guid movieId, DateTime? from = null, DateTime? to = null)
        {
            var query = await _userMovieInteractionRepository.GetQueryableAsync();

            // إذا كانت تواريخ البداية والنهاية محددة
            if (from.HasValue && to.HasValue)
            {
                return query.Count(umi => umi.MovieId == movieId
                                          && umi.Interaction == InteractionType.Downloaded
                                          && umi.CreationTime >= from.Value
                                          && umi.CreationTime <= to.Value);
            }

            // إذا لم يتم تحديد نطاق زمني
            return query.Count(umi => umi.MovieId == movieId
                                      && umi.Interaction == InteractionType.Downloaded);
        }

        public async Task<ListResultDto<ActorLookupDto>> GetActorLookupAsync(string searchTerm)
        {
            var query = await _actorRepository.GetQueryableAsync();


            // إذا كان هناك مصطلح بحث، قم بتصفية الممثلين بناءً عليه
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(a => a.ActorName.Contains(searchTerm)); // استبدل "Name" باسم الخاصية المناسبة
            }

            // احصل على أول 10 ممثلين
            var actors = await query.Take(10).ToListAsync();

            return new ListResultDto<ActorLookupDto>(
                ObjectMapper.Map<List<Actor>, List<ActorLookupDto>>(actors)
            );
        }

        public async Task<ListResultDto<CategoryLookupDto>> GetCategoryLookupAsync(string searchTerm)
        {
            var query = await _categoryRepository.GetQueryableAsync();


            // إذا كان هناك مصطلح بحث، قم بتصفية التصنيفات بناءً عليه
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => c.CategoryName.Contains(searchTerm)); // استبدل "Name" باسم الخاصية المناسبة
            }

            // احصل على أول 10 تصنيفات
            var categories = await query.Take(10).ToListAsync();

            return new ListResultDto<CategoryLookupDto>(
                ObjectMapper.Map<List<Category>, List<CategoryLookupDto>>(categories)
            );
        }
        private async Task<string> UploadFileAsync(IRemoteStreamContent blob)
        {
            var name = blob.FileName + " - " +Guid.NewGuid().ToString();
            await _blobContainer.SaveAsync(name, await blob.GetStream().GetAllBytesAsync());
            return name;
        }

        public async Task<byte[]> GetBytesAsync()
        {
            return await _blobContainer.GetAllBytesOrNullAsync("my-blob-1");
        }

    }
}
