using MovieManagementApp.Actors;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MovieManagementApp.Application.Contracts.Actors
{
    public interface IActorAppService :
        ICrudAppService< // Defines CRUD methods
            ActorDto, // Used to show actors
            Guid, // Primary key of the actor entity
            PagedAndSortedResultRequestDto, // Used for paging/sorting
            CreateUpdateActorDto> // Used to create/update an actor
    {
    }
}
