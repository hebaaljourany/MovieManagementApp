using System;
using Volo.Abp.Domain.Repositories;

namespace MovieManagementApp.Movies
{
    public interface IMovieRepository : IRepository<Movie, Guid>
    {
        // يمكنك إضافة استعلامات مخصصة هنا إذا كنت بحاجة إليها
    }
}
