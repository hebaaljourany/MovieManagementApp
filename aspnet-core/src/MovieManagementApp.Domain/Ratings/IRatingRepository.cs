using System;
using Volo.Abp.Domain.Repositories;

namespace MovieManagementApp.Ratings
{
    public interface IRatingRepository : IRepository<Rating, Guid>
    {
        // يمكنك إضافة استعلامات مخصصة هنا إذا كنت بحاجة إليها
    }
}
