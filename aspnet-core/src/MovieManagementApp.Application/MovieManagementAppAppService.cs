using System;
using System.Collections.Generic;
using System.Text;
using MovieManagementApp.Localization;
using Volo.Abp.Application.Services;

namespace MovieManagementApp;

/* Inherit your application services from this class.
 */
public abstract class MovieManagementAppAppService : ApplicationService
{
    protected MovieManagementAppAppService()
    {
        LocalizationResource = typeof(MovieManagementAppResource);
    }
}
