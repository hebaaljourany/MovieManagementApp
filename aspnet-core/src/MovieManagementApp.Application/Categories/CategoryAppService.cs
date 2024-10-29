using MovieManagementApp.Application.Contracts.Categories;
using MovieManagementApp.Permissions;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MovieManagementApp.Categories
{
    public class CategoryAppService :
        CrudAppService<
            Category, // The Category entity
            CategoryDto, // Used to show categories
            Guid, // Primary key of the category entity
            PagedAndSortedResultRequestDto, // Used for paging/sorting
            CreateUpdateCategoryDto>, // Used to create/update a category
        ICategoryAppService // Implement the ICategoryAppService
    {
        public CategoryAppService(IRepository<Category, Guid> repository)
            : base(repository)
        {
            GetPolicyName = MovieManagementAppPermissions.Categories.Default;
            GetListPolicyName = MovieManagementAppPermissions.Categories.Default;
            CreatePolicyName = MovieManagementAppPermissions.Categories.Create;
            UpdatePolicyName = MovieManagementAppPermissions.Categories.Edit;
            DeletePolicyName = MovieManagementAppPermissions.Categories.Delete;
        }
    }
}
