using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MovieManagementApp.Data;

/* This is used if database provider does't define
 * IMovieManagementAppDbSchemaMigrator implementation.
 */
public class NullMovieManagementAppDbSchemaMigrator : IMovieManagementAppDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
