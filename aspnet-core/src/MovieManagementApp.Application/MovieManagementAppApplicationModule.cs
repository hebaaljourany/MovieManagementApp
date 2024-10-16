using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Microsoft.AspNetCore.Authorization;
using MovieManagementApp.Controllers;
using Volo.Abp.AspNetCore.Mvc;
using MovieManagementApp.MyAccounts;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace MovieManagementApp;

[DependsOn(
    typeof(MovieManagementAppDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(MovieManagementAppApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class MovieManagementAppApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MovieManagementAppApplicationModule>();
        });
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(MyAccountController).Assembly);
        });
        context.Services.AddTransient<AccountAppService, MyAccountAppService>();
        context.Services.Replace(
             ServiceDescriptor.Transient<IAccountAppService, MyAccountAppService>());

    }
}
