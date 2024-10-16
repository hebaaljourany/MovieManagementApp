using MovieManagementApp.Categories;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MovieManagementApp.Application.Contracts.Categories
{
    public interface ICategoryAppService :
        ICrudAppService< // Defines CRUD methods
            CategoryDto, // Used to show categories
            Guid, // Primary key of the category entity
            PagedAndSortedResultRequestDto, // Used for paging/sorting
            CreateUpdateCategoryDto> // Used to create/update a category
    {
    }
}
