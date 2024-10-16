using MovieManagementApp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MovieManagementApp;

[DependsOn(
    typeof(MovieManagementAppEntityFrameworkCoreTestModule)
    )]
public class MovieManagementAppDomainTestModule : AbpModule
{

}
