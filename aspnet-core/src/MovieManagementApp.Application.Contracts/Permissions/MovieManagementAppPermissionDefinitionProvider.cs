using MovieManagementApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MovieManagementApp.Permissions;

public class MovieManagementAppPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MovieManagementAppPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(MovieManagementAppPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MovieManagementAppResource>(name);
    }
}
