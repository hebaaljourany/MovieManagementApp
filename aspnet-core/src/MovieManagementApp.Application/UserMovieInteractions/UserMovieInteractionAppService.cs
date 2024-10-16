using MovieManagementApp.Application.Contracts.UserMovieInteractions;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MovieManagementApp.UserMovieInteractions
{
    public class UserMovieInteractionAppService :
        CrudAppService<
            UserMovieInteraction, // The UserMovieInteraction entity
            UserMovieInteractionDto, // Used to show user movie interactions
            Guid, // Primary key of the user movie interaction entity
            PagedAndSortedResultRequestDto, // Used for paging/sorting
            CreateUpdateUserMovieInteractionDto>, // Used to create/update a user movie interaction
        IUserMovieInteractionAppService // Implement the IUserMovieInteractionAppService
    {
        public UserMovieInteractionAppService(IRepository<UserMovieInteraction, Guid> repository)
            : base(repository)
        {
        }
    }
}
