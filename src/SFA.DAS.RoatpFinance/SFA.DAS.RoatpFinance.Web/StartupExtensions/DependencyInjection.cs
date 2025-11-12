using System.Security.Claims;
using Microsoft.AspNetCore.Builder;

namespace SFA.DAS.RoatpFinance.Web.StartupExtensions;

public static class DependencyInjection
{
    public static void ConfigureDependencyInjection(IServiceCollection services)
    {

        UserExtensions.Logger = services.BuildServiceProvider().GetService<ILogger<ClaimsPrincipal>>();
    }
}
