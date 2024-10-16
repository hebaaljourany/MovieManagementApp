using System.Threading.Tasks;

namespace MovieManagementApp.Data;

public interface IMovieManagementAppDbSchemaMigrator
{
    Task MigrateAsync();
}
