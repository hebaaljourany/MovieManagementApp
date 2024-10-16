using Volo.Abp.Settings;

namespace MovieManagementApp.Settings;

public class MovieManagementAppSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MovieManagementAppSettings.MySetting1));
    }
}
