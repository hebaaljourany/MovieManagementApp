using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieManagementApp.Data;
using Volo.Abp.DependencyInjection;

namespace MovieManagementApp.EntityFrameworkCore;

public class EntityFrameworkCoreMovieManagementAppDbSchemaMigrator
    : IMovieManagementAppDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreMovieManagementAppDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the MovieManagementAppDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<MovieManagementAppDbContext>()
            .Database
            .MigrateAsync();
    }
}
