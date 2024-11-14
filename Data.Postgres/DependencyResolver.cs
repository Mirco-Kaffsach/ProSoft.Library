using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProSoft.Library.Data.Postgres;

public static class DependencyResolver
{
	public static IServiceCollection AddProSoftLibraryPostgresDatabase(this IServiceCollection services)
	{
		Guard.Against.Null(services);

		services
			.AddDbContextFactory<LibraryDbContext>(context => context.UseNpgsql(DatabaseConfig.ConnectionString))
			;

		return services;
	}

	public static IHost UseProSoftLibraryPostgresDatabase(this IHost host, ILogger logger)
	{
		Guard.Against.Null(host);
		Guard.Against.Null(logger);

		logger.LogInformation("Using Postgres database");
		MigrationHelper.Migrate<LibraryDbContext>(host);

		return host;
	}
}