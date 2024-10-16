using MovieManagementApp.Application.Contracts.Actors;
using System;
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
        public ActorAppService(IRepository<Actor, Guid> repository)
            : base(repository)
        {
        }
    }
}
