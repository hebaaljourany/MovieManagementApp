using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace MovieManagementApp;

[Dependency(ReplaceServices = true)]
public class MovieManagementAppBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "MovieManagementApp";
}
