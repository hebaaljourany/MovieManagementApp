using System;
using Volo.Abp.Domain.Repositories;

namespace MovieManagementApp.MovieCategories
{
    public interface IMovieCategoryRepository : IRepository<MovieCategory, Guid>
    {
        // يمكنك إضافة استعلامات مخصصة هنا إذا كنت بحاجة إليها
    }
}
