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
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Minio;
using MovieManagementApp.Application.Contracts.Movies;
using Volo.Abp.Content;

namespace MovieManagementApp;

[DependsOn(
    typeof(MovieManagementAppDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(MovieManagementAppApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpBlobStoringModule),
    typeof(AbpBlobStoringMinioModule))]
    public class MovieManagementAppApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureDefault(container =>
            {
                container.UseMinio(minio =>
                {
                    minio.EndPoint = "192.168.43.139:9000";
                    minio.AccessKey = "JlJoOgQlyH6LcvalYVAL";
                    minio.SecretKey = "GMqJ3qO4foNgWXE6v5W4rVPYUlTK71ntKwlpFx2i";
                    minio.BucketName = "movies";
                });
            });
        });
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MovieManagementAppApplicationModule>();
        });
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(MyAccountController).Assembly);
            options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(CreateUpdateMovieDto));
        });
        context.Services.AddTransient<AccountAppService, MyAccountAppService>();
        context.Services.Replace(
             ServiceDescriptor.Transient<IAccountAppService, MyAccountAppService>());

    }
}
