using Microsoft.AspNetCore.Mvc;
using MovieManagementApp.Application.Contracts.Actors;
using MovieManagementApp.Application.Contracts.Movies;
using MovieManagementApp.Blob;
using MovieManagementApp.Categories;
using MovieManagementApp.Helpers;
using MovieManagementApp.MovieActors;
using MovieManagementApp.MovieCategories;
using MovieManagementApp.Movies;
using MovieManagementApp.Permissions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Content;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using static MovieManagementApp.Permissions.MovieManagementAppPermissions;

namespace MovieManagementApp.Actors
{
    public class ActorAppService :
        CrudAppService<
            Actor, // The Actor entity
            ActorDto, // Used to show actors
            Guid, // Primary key of the actor entity
            PagedAndSortedResultRequestDto, // Used for paging/sorting
            CreateUpdateActorDto>, // Used to create/update an actor
        IActorAppService // Implement the IActorAppService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IBlobContainer<MovieContainer> _blobContainer;


        public ActorAppService(
            IRepository<Actor, Guid> repository,
            IActorRepository actorRepository,
            IBlobContainer<MovieContainer> blobContainer

            )
            : base(repository)
        {
            _actorRepository = actorRepository;
            GetPolicyName = MovieManagementAppPermissions.Actors.Default;
            GetListPolicyName = MovieManagementAppPermissions.Actors.Default;
            CreatePolicyName = MovieManagementAppPermissions.Actors.Create;
            UpdatePolicyName = MovieManagementAppPermissions.Actors.Edit;
            DeletePolicyName = MovieManagementAppPermissions.Actors.Delete;
            _blobContainer = blobContainer;


        }
        public override async Task<ActorDto> GetAsync(Guid id)
        {

            // Get the IQueryable<Movie> from the repository
            var queryable = await Repository.GetQueryableAsync();

            // تأكد من أن هناك فيلمًا بهذا المعرّف
            var actor = await AsyncExecuter.FirstOrDefaultAsync(queryable.Where(m => m.Id == id));
            if (actor == null)
            {
                throw new EntityNotFoundException(typeof(Actor), id);
            }

            // Map movie to DTO
            var actorDto = ObjectMapper.Map<Actor, ActorDto>(actor);
           
            var actorImageBlob = await _blobContainer.GetAllBytesAsync(actor.ActorImageBlob);

            // Include average rating if needed

            return actorDto;

        }
        public override async Task<ActorDto> CreateAsync([FromForm] CreateUpdateActorDto input)
        {

            var actor = ObjectMapper.Map<CreateUpdateActorDto, Actor>(input);


            var ActorImageBlobName = await UploadFileAsync(input.ActorImageBlob, 273, 184);


            actor.ActorImageBlob = ActorImageBlobName;
            var bytes = await input.ActorImageBlob.GetStream().GetAllBytesAsync();
            var base64 = Convert.ToBase64String(bytes);
            actor.Thumbnail = ThumbnailGenerator.CreateThumbnailFromBase64(base64, 75, 75);

            actor = await Repository.InsertAsync(actor, true);


            return ObjectMapper.Map<Actor, ActorDto>(actor);
        }
        public override async Task<ActorDto> UpdateAsync(Guid id, [FromForm] CreateUpdateActorDto input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");
            }
            var actor = await Repository.GetAsync(id);
            actor.ActorName = input.ActorName;
            string actorBlobName;

            if (input.ActorImageBlob != null)
            {
                actorBlobName = await UploadFileAsync(input.ActorImageBlob, null, null);
                actor.ActorImageBlob = actorBlobName;

            
            
            var bytes = await input.ActorImageBlob.GetStream().GetAllBytesAsync();
            var base64 = Convert.ToBase64String(bytes);
            actor.Thumbnail = ThumbnailGenerator.CreateThumbnailFromBase64(base64, 75, 75);
            }
            var updatedActor = await Repository.UpdateAsync(actor, true);


            return ObjectMapper.Map<Actor, ActorDto>(updatedActor);
            // 7. حفظ التحديثات في قاعدة البيانات

            // 8. إرجاع الكائن المحدث
        }

        private async Task<string> UploadFileAsync(IRemoteStreamContent blob, int? height, int? width)
        {
            var name = blob.FileName + " - " + Guid.NewGuid().ToString();

            if (height.HasValue && width.HasValue)
            {
                var base64 = ThumbnailGenerator.GetBase64(await blob.GetStream().GetAllBytesAsync());
                var resizedImage = ThumbnailGenerator.CreateThumbnailFromBase64(base64, height.Value, width.Value);
                var bytes = ThumbnailGenerator.GetBytes(resizedImage);
                await _blobContainer.SaveAsync(name, bytes);
                return name;
            }

            await _blobContainer.SaveAsync(name, await blob.GetStream().GetAllBytesAsync());
            return name;

        }

        public async Task<byte[]> GetBytesAsync()
        {
            return await _blobContainer.GetAllBytesOrNullAsync("my-blob-1");
        }

    }
}
