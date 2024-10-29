using MovieManagementApp.Application.Contracts.Actors;
using MovieManagementApp.Application.Contracts.Movies;
using MovieManagementApp.Movies;
using MovieManagementApp.Permissions;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

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

        public ActorAppService(
            IRepository<Actor, Guid> repository,
            IActorRepository actorRepository

            )
            : base(repository)
        {
            _actorRepository = actorRepository;
            GetPolicyName = MovieManagementAppPermissions.Actors.Default;
            GetListPolicyName = MovieManagementAppPermissions.Actors.Default;
            CreatePolicyName = MovieManagementAppPermissions.Actors.Create;
            UpdatePolicyName = MovieManagementAppPermissions.Actors.Edit;
            DeletePolicyName = MovieManagementAppPermissions.Actors.Delete;

        }
    }
}
