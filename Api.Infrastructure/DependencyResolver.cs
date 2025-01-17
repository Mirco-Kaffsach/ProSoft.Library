using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProSoft.Library.Core.Infrastructure;

namespace ProSoft.Library.Api.Infrastructure;

public static class DependencyResolver
{
	public static IServiceCollection AddProSoftLibrary(this IServiceCollection services, string databaseEngine)
	{
		Guard.Against.Null(services);
		Guard.Against.NullOrWhiteSpace(databaseEngine);

		services
			.AddProSoftLibraryCore(databaseEngine)
			;

		return services;
	}

	public static IHost UseProSoftLibrary(this IHost host, ILogger logger, string databaseEngine)
	{
		Guard.Against.Null(host);
		Guard.Against.Null(logger);
		
		logger.LogInformation("Configuring dependencies for: ProSoft.Library.Api");

		host
			.UseProSoftLibraryCore(logger, databaseEngine)
			;

		return host;
	}
}
