using MovieManagementApp.Application.Contracts.Categories;
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
        }
    }
}
