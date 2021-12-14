using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace DotNetCoreSqlDb.Telemetry
{
  /*
   * SqlParameters TelemetryInitializer that overrides the default SDK
   * behavior of tracking SQL Parameters
   *
   */
  public class SqlParametersTelemetryInitializer : ITelemetryInitializer
  {
    public void Initialize(ITelemetry telemetry)
    {
      if (telemetry is DependencyTelemetry dependencyTelemetry && dependencyTelemetry .Type == "SQL")
      {
          if (dependencyTelemetry.TryGetOperationDetail("SqlCommand", out var command)
              && command is SqlCommand sqlCommand)
          {
              foreach (DbParameter parameter in ((SqlCommand)command).Parameters)
              {
                  dependencyTelemetry.Properties.Add(parameter.ParameterName, parameter.Value.ToString());
              }
          }
      }
    }
  }
}