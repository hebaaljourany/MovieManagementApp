using System;
using Volo.Abp.Domain.Repositories;

namespace MovieManagementApp.Categories
{
    public interface IMovieCategoryRepository : IRepository<Category, Guid>
    {
        // يمكنك إضافة استعلامات مخصصة هنا إذا كنت بحاجة إليها
    }
}
