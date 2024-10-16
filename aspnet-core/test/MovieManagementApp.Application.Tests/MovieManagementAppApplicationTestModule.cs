using Volo.Abp.Modularity;

namespace MovieManagementApp;

[DependsOn(
    typeof(MovieManagementAppApplicationModule),
    typeof(MovieManagementAppDomainTestModule)
    )]
public class MovieManagementAppApplicationTestModule : AbpModule
{

}
