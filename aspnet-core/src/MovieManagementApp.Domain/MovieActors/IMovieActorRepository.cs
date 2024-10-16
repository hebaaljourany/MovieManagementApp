using System;
using Volo.Abp.Domain.Repositories;

namespace MovieManagementApp.MovieActors
{
    public interface IMovieActorRepository : IRepository<MovieActor, Guid>
    {
        // يمكنك إضافة استعلامات مخصصة هنا إذا كنت بحاجة إليها
    }
}
