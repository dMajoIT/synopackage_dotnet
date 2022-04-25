using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Linq;

namespace Synopackage.Web.HealthChecks
{
  public static class DependencyInjection
  {
    public static IHealthChecksBuilder AddSynopackageSourcesHealthChecks(this IHealthChecksBuilder hcBuilder)
    {
      var builder = hcBuilder;
      if (AppSettingsProvider.AppSettings.HealthChecks.Enabled)
        foreach (var sourceName in Model.SourceHelper.ActiveSources.Where(p => !p.IsOfficial).Select(p => p.Name))
        {
          builder = builder.AddTypeActivatedCheck<SourceHealthCheck>(
            $"Repository: {sourceName}",
            HealthStatus.Degraded,
            tags: new[] { "source" },
            args: new[] { sourceName }
            );
        }
      return builder;
    }
  }
}
