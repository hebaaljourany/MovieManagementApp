using System;
using Volo.Abp.Domain.Repositories;

namespace MovieManagementApp.UserMovieInteractions
{
    public interface IUserMovieInteractionRepository : IRepository<UserMovieInteraction, Guid>
    {
        // يمكنك إضافة استعلامات مخصصة هنا إذا كنت بحاجة إليها
    }
}
