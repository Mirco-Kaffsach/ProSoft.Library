using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProSoft.Library.Core.Exceptions;
using ProSoft.Library.Data.Contracts;
using ProSoft.Library.Data.Postgres;
using ProSoft.Library.Data.Postgres.Engines;

namespace ProSoft.Library.Data.Infrastructure;

public static class DependencyResolver
{
	public static IServiceCollection AddProSoftLibraryData(this IServiceCollection services, string databaseEngine)
	{
		Guard.Against.Null(services);
		Guard.Against.NullOrWhiteSpace(databaseEngine);

		switch (databaseEngine.ToLowerInvariant())
		{
			case "postgres":
				services.AddProSoftLibraryPostgresDatabase();
				break;
			
			default:
				throw new DatabaseEngineNotSupportedException($"Database engine '{databaseEngine}' is not supported.");
		}

		services
			.AddScoped<ITagEngine, TagEngine>()
			;

		return services;
	}

	public static IHost UseProSoftLibraryData(this IHost host, ILogger logger, string databaseEngine)
	{
		Guard.Against.Null(host);
		Guard.Against.Null(logger);
		Guard.Against.NullOrWhiteSpace(databaseEngine);

		logger.LogInformation("Configure dependencies for: ProSoft.Library.Data");

		switch (databaseEngine.ToLowerInvariant())
		{
			case "postgres":
				host.UseProSoftLibraryPostgresDatabase(logger);
				break;

			default:
				throw new DatabaseEngineNotSupportedException($"Database engine '{databaseEngine}' is not supported.");
		}

		return host;
	}
}