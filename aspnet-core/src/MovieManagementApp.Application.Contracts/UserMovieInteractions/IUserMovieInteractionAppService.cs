using MovieManagementApp.UserMovieInteractions;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MovieManagementApp.Application.Contracts.UserMovieInteractions
{
    public interface IUserMovieInteractionAppService :
        ICrudAppService< // Defines CRUD methods
            UserMovieInteractionDto, // Used to show user movie interactions
            Guid, // Primary key of the user movie interaction entity
            PagedAndSortedResultRequestDto, // Used for paging/sorting
            CreateUpdateUserMovieInteractionDto> // Used to create/update a user movie interaction
    {
    }
}
