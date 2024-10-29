using MovieManagementApp.Application.Contracts.Ratings;
using MovieManagementApp.Permissions;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MovieManagementApp.Ratings
{
    public class RatingAppService :
        CrudAppService<
            Rating, // The Rating entity
            RatingDto, // Used to show ratings
            Guid, // Primary key of the rating entity
            PagedAndSortedResultRequestDto, // Used for paging/sorting
            CreateUpdateRatingDto>, // Used to create/update a rating
        IRatingAppService // Implement the IRatingAppService
    {
        public RatingAppService(IRepository<Rating, Guid> repository)
            : base(repository)
        {
            GetPolicyName = MovieManagementAppPermissions.Ratings.Default;
            GetListPolicyName = MovieManagementAppPermissions.Ratings.Default;
            CreatePolicyName = MovieManagementAppPermissions.Ratings.Create;
            UpdatePolicyName = MovieManagementAppPermissions.Ratings.Edit;
            DeletePolicyName = MovieManagementAppPermissions.Ratings.Delete;
        }
    }
}
