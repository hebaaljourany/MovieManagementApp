using System;
using Volo.Abp.Domain.Repositories;

namespace MovieManagementApp.Actors
{
    public interface IMovieActorRepository : IRepository<Actor, Guid>
    {
        // يمكنك إضافة استعلامات مخصصة هنا إذا كنت بحاجة إليها
    }
}
