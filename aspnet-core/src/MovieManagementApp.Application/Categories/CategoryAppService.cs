using MovieManagementApp.Actors;
using MovieManagementApp.Application.Contracts.Categories;
using MovieManagementApp.MovieCategories;
using MovieManagementApp.Permissions;
using System;
using System.Threading.Tasks;
using Volo.Abp;
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
        private readonly IRepository<MovieCategory, Guid> _movieCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;


        public CategoryAppService(
            IRepository<Category, Guid> repository,
            ICategoryRepository categoryRepository,
            IRepository<MovieCategory, Guid> movieCategoryRepository

)
            : base(repository)
        {
            _categoryRepository = categoryRepository;

            GetPolicyName = MovieManagementAppPermissions.Categories.Default;
            GetListPolicyName = MovieManagementAppPermissions.Categories.Default;
            CreatePolicyName = MovieManagementAppPermissions.Categories.Create;
            UpdatePolicyName = MovieManagementAppPermissions.Categories.Edit;
            DeletePolicyName = MovieManagementAppPermissions.Categories.Delete;
            _movieCategoryRepository = movieCategoryRepository;

        }

        private async Task CheckCategoryNameExistsAsync(string categoryName, Guid? id = null)
        {
            var existingCategory = await _categoryRepository.FirstOrDefaultAsync(
                a => a.CategoryName.ToLower() == categoryName.ToLower() && (!id.HasValue || a.Id != id.Value)
            );

            if (existingCategory != null)
            {
                throw new UserFriendlyException("An category with this name already exists. Please choose a different name.");
            }
        }

        public override async Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input)
        {
            await CheckCategoryNameExistsAsync(input.CategoryName);


            var category = ObjectMapper.Map<CreateUpdateCategoryDto, Category>(input);
            category = await Repository.InsertAsync(category, true);

            return ObjectMapper.Map<Category, CategoryDto>(category);
        }

        public override async Task<CategoryDto> UpdateAsync(Guid id, CreateUpdateCategoryDto input)
        {
            await CheckCategoryNameExistsAsync(input.CategoryName, id);


            var category = await Repository.GetAsync(id);
            category.CategoryName = input.CategoryName;

            var updatedCategory = await Repository.UpdateAsync(category, true);

            return ObjectMapper.Map<Category, CategoryDto>(updatedCategory);
        }


        public override async Task DeleteAsync(Guid id)
        {
            // تحقق من وجود الأفلام المرتبطة بهذا التصنيف
            var isCategoryLinked = await _movieCategoryRepository
                .AnyAsync(mc => mc.CategoryId == id);

            // إذا كان التصنيف مرتبطًا بأفلام، ارمي استثناء برمز الخطأ ورسالة للمستخدم
            if (isCategoryLinked)
            {
                throw new UserFriendlyException("Cannot delete this category. Please remove the associated movies first.");
            }

            // إذا لم يكن التصنيف مرتبطًا، قم بحذفه
            await base.DeleteAsync(id);
        }
    }


}

