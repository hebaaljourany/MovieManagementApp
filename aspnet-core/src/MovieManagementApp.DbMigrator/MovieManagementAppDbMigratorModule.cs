using MovieManagementApp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace MovieManagementApp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MovieManagementAppEntityFrameworkCoreModule),
    typeof(MovieManagementAppApplicationContractsModule)
    )]
public class MovieManagementAppDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
