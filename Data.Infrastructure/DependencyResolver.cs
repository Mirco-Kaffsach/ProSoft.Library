using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProSoft.Library.Data.Postgres;

namespace ProSoft.Library.Data.Infrastructure;

public static class DependencyResolver
{
	public static IServiceCollection AddProSoftLibraryData(this IServiceCollection services)
	{
		Guard.Against.Null(services);

		services
			.AddProSoftLibraryPostgresDatabase()
			;

		return services;
	}

	public static IHost UseProSoftLibraryData(this IHost host, ILogger logger)
	{
		Guard.Against.Null(host);
		Guard.Against.Null(logger);

		logger.LogInformation("Configure dependencies for: ProSoft.Library.Data");

		host
			.UseProSoftLibraryPostgresDatabase(logger)
			;

		return host;
	}
}