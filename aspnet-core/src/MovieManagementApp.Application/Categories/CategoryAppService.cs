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

        public CategoryAppService(
            IRepository<Category, Guid> repository,
            IRepository<MovieCategory, Guid> movieCategoryRepository

)
            : base(repository)
        {
            GetPolicyName = MovieManagementAppPermissions.Categories.Default;
            GetListPolicyName = MovieManagementAppPermissions.Categories.Default;
            CreatePolicyName = MovieManagementAppPermissions.Categories.Create;
            UpdatePolicyName = MovieManagementAppPermissions.Categories.Edit;
            DeletePolicyName = MovieManagementAppPermissions.Categories.Delete;
            _movieCategoryRepository = movieCategoryRepository;

        }


        public override async Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input)
        {
            // تحقق من وجود تصنيف بنفس الاسم
            var existingCategory = await Repository.FirstOrDefaultAsync(c => c.CategoryName.Equals(input.CategoryName, StringComparison.OrdinalIgnoreCase));
            if (existingCategory != null)
            {
                throw new UserFriendlyException("A category with this name already exists. Please choose a different name.");
            }

            var category = ObjectMapper.Map<CreateUpdateCategoryDto, Category>(input);
            category = await Repository.InsertAsync(category, true);

            return ObjectMapper.Map<Category, CategoryDto>(category);
        }

        public override async Task<CategoryDto> UpdateAsync(Guid id, CreateUpdateCategoryDto input)
        {
            // تحقق من وجود تصنيف بنفس الاسم ولكن استثني التصنيف الحالي
            var existingCategory = await Repository.FirstOrDefaultAsync(c => c.CategoryName.Equals(input.CategoryName, StringComparison.OrdinalIgnoreCase) && c.Id != id);
            if (existingCategory != null)
            {
                throw new UserFriendlyException("A category with this name already exists. Please choose a different name.");
            }

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

