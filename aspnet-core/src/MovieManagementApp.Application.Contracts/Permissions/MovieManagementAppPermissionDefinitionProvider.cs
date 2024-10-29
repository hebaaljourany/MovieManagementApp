using MovieManagementApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MovieManagementApp.Permissions;

public class MovieManagementAppPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MovieManagementAppPermissions.GroupName);

        var moviesPermission = myGroup.AddPermission(MovieManagementAppPermissions.Movies.Default, L("Permission:Movies"));
        moviesPermission.AddChild(MovieManagementAppPermissions.Movies.Create, L("Permission:Movies.Create"));
        moviesPermission.AddChild(MovieManagementAppPermissions.Movies.Edit, L("Permission:Movies.Edit"));
        moviesPermission.AddChild(MovieManagementAppPermissions.Movies.Delete, L("Permission:Movies.Delete"));


        var actorsPermission = myGroup.AddPermission(MovieManagementAppPermissions.Actors.Default, L("Permission:Actors"));
        actorsPermission.AddChild(MovieManagementAppPermissions.Actors.Create, L("Permission:Actors.Create"));
        actorsPermission.AddChild(MovieManagementAppPermissions.Actors.Edit, L("Permission:Actors.Edit"));
        actorsPermission.AddChild(MovieManagementAppPermissions.Actors.Delete, L("Permission:Actors.Delete"));

        var categoriesPermission = myGroup.AddPermission(MovieManagementAppPermissions.Categories.Default, L("Permission:Categories"));
        categoriesPermission.AddChild(MovieManagementAppPermissions.Categories.Create, L("Permission:Categories.Create"));
        categoriesPermission.AddChild(MovieManagementAppPermissions.Categories.Edit, L("Permission:Categories.Edit"));
        categoriesPermission.AddChild(MovieManagementAppPermissions.Categories.Delete, L("Permission:Categories.Delete"));

        var myListsPermission = myGroup.AddPermission(MovieManagementAppPermissions.MyLists.Default, L("Permission:MyLists"));
        myListsPermission.AddChild(MovieManagementAppPermissions.MyLists.Create, L("Permission:MyLists.Create"));
        myListsPermission.AddChild(MovieManagementAppPermissions.MyLists.Edit, L("Permission:MyLists.Edit"));
        myListsPermission.AddChild(MovieManagementAppPermissions.MyLists.Delete, L("Permission:MyLists.Delete"));

        var ratingsPermission = myGroup.AddPermission(MovieManagementAppPermissions.Ratings.Default, L("Permission:Ratings"));
        ratingsPermission.AddChild(MovieManagementAppPermissions.Ratings.Create, L("Permission:Ratings.Create"));
        ratingsPermission.AddChild(MovieManagementAppPermissions.Ratings.Edit, L("Permission:Ratings.Edit"));
        ratingsPermission.AddChild(MovieManagementAppPermissions.Ratings.Delete, L("Permission:Ratings.Delete"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(MovieManagementAppPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MovieManagementAppResource>(name);
    }
}
