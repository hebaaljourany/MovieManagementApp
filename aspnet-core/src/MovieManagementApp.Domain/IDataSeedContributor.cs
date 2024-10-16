using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using MovieManagementApp.MyAccounts;
using MovieManagementApp.Actors;
using MovieManagementApp.Categories;
using MovieManagementApp.MovieActors;
using MovieManagementApp.MovieCategories;
using MovieManagementApp.Movies;
using MovieManagementApp.MyLists;
using MovieManagementApp.Ratings;
using MovieManagementApp.UserMovieInteractions;

namespace MovieManagementApp
{
    public class MovieManagementDataSeederContributor
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<MyAccount, Guid> _myAccountRepository;
        private readonly IRepository<Actor, Guid> _actorRepository;
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<Movie, Guid> _movieRepository;
        private readonly IRepository<MovieActor, Guid> _movieActorRepository;
        private readonly IRepository<MovieCategory, Guid> _movieCategoryRepository;
        private readonly IRepository<MyList, Guid> _myListRepository;
        private readonly IRepository<Rating, Guid> _ratingRepository;
        private readonly IRepository<UserMovieInteraction, Guid> _userMovieInteractionRepository;
        private readonly IIdentityUserRepository _userRepository; // إضافة

        public MovieManagementDataSeederContributor(
            IRepository<MyAccount, Guid> myAccountRepository,
            IRepository<Actor, Guid> actorRepository,
            IRepository<Category, Guid> categoryRepository,
            IRepository<Movie, Guid> movieRepository,
            IRepository<MovieActor, Guid> movieActorRepository,
            IRepository<MovieCategory, Guid> movieCategoryRepository,
            IRepository<MyList, Guid> myListRepository,
            IRepository<Rating, Guid> ratingRepository,
            IRepository<UserMovieInteraction, Guid> userMovieInteractionRepository,
            IIdentityUserRepository userRepository) // إضافة
        {
            _myAccountRepository = myAccountRepository;
            _actorRepository = actorRepository;
            _categoryRepository = categoryRepository;
            _movieRepository = movieRepository;
            _movieActorRepository = movieActorRepository;
            _movieCategoryRepository = movieCategoryRepository;
            _myListRepository = myListRepository;
            _ratingRepository = ratingRepository;
            _userMovieInteractionRepository = userMovieInteractionRepository;
            _userRepository = userRepository; // إضافة
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            // Check if data already exists
            if (await _movieRepository.GetCountAsync() > 0)
            {
                return;
            }

            // Create Users
                var user1 = await _userRepository.InsertAsync(
                new IdentityUser(
                    Guid.NewGuid(), // إنشاء معرف GUID جديد
                    "user1",
                    "user1@domain.com"
                ),
                autoSave: true
                );

               var user2 = await _userRepository.InsertAsync(
               new IdentityUser(
                    Guid.NewGuid(), // إنشاء معرف GUID جديد
                    "user2",
                    "user2@domain.com"
                    ),
                    autoSave: true
                    );
            // Create MyAccounts with valid UserIds
            var myaccount1 = await _myAccountRepository.InsertAsync(
                new MyAccount { UserId = user1.Id }, // استخدام UserId الذي تم إنشاؤه
                autoSave: true

            );

            var myaccount2 = await _myAccountRepository.InsertAsync(
                new MyAccount { UserId = user2.Id }, // استخدام UserId الذي تم إنشاؤه
                autoSave: true
            );

            // Create Actors
            var actor1 = await _actorRepository.InsertAsync(
                new Actor { ActorName = "Leonardo DiCaprio" },
                autoSave: true
            );

            var actor2 = await _actorRepository.InsertAsync(
                new Actor { ActorName = "Scarlett Johansson" },
                autoSave: true
            );

            // Create Categories
            var category1 = await _categoryRepository.InsertAsync(
                new Category { CategoryName = "Action" },
                autoSave: true
            );

            var category2 = await _categoryRepository.InsertAsync(
                new Category { CategoryName = "Drama" },
                autoSave: true
            );

            // Create Movies
            var movie1 = await _movieRepository.InsertAsync(
                new Movie
                {
                    Title = "Inception",
                    ReleaseDate = new DateTime(2010, 7, 16),
                    Duration = 148,
                    Description = "A mind-bending thriller",
                    AgeRating = "PG-13",
                    PosterUrl = "https://example.com/inception.jpg",
                    VideoUrl = "https://example.com/inception.mp4"
                },
                autoSave: true
            );

            var movie2 = await _movieRepository.InsertAsync(
                new Movie
                {
                    Title = "Black Widow",
                    ReleaseDate = new DateTime(2021, 7, 9),
                    Duration = 134,
                    Description = "A superhero action film",
                    AgeRating = "PG-13",
                    PosterUrl = "https://example.com/blackwidow.jpg",
                    VideoUrl = "https://example.com/blackwidow.mp4"
                },
                autoSave: true
            );

            // Create MovieActors
            await _movieActorRepository.InsertAsync(
                new MovieActor
                {
                    MovieId = movie1.Id,
                    ActorId = actor1.Id
                },
                autoSave: true
            );

            await _movieActorRepository.InsertAsync(
                new MovieActor
                {
                    MovieId = movie2.Id,
                    ActorId = actor2.Id
                },
                autoSave: true
            );

            // Create MovieCategories
            await _movieCategoryRepository.InsertAsync(
                new MovieCategory
                {
                    MovieId = movie1.Id,
                    CategoryId = category1.Id
                },
                autoSave: true
            );

            await _movieCategoryRepository.InsertAsync(
                new MovieCategory
                {
                    MovieId = movie2.Id,
                    CategoryId = category2.Id
                },
                autoSave: true
            );

            // Create MyLists
            await _myListRepository.InsertAsync(
                new MyList
                {
                    MyAccountId = myaccount1.Id,
                    MovieId = movie1.Id
                },
                autoSave: true
            );

            await _myListRepository.InsertAsync(
                new MyList
                {
                    MyAccountId = myaccount2.Id,
                    MovieId = movie2.Id
                },
                autoSave: true
            );

            // Create Ratings
            await _ratingRepository.InsertAsync(
                new Rating
                {
                    MyAccountId = myaccount1.Id,
                    MovieId = movie1.Id,
                    RatingValue = 5
                },
                autoSave: true
            );

            await _ratingRepository.InsertAsync(
                new Rating
                {
                    MyAccountId = myaccount2.Id,
                    MovieId = movie2.Id,
                    RatingValue = 4
                },
                autoSave: true
            );

            // Create UserMovieInteractions
            await _userMovieInteractionRepository.InsertAsync(
                new UserMovieInteraction
                {
                    MyAccountId = myaccount1.Id,
                    MovieId = movie1.Id,
                    Interaction = InteractionType.Watched
                },
                autoSave: true
            );

            await _userMovieInteractionRepository.InsertAsync(
                new UserMovieInteraction
                {
                    MyAccountId = myaccount2.Id,
                    MovieId = movie2.Id,
                    Interaction = InteractionType.Downloaded
                },
                autoSave: true
            );
        }
    }
}
