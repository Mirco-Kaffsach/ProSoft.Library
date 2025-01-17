using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProSoft.Library.Core.Contracts;
using ProSoft.Library.Core.Manager;
using ProSoft.Library.Data.Infrastructure;

namespace ProSoft.Library.Core.Infrastructure;

public static class DependencyResolver
{
	public static IServiceCollection AddProSoftLibraryCore(this IServiceCollection services, string databaseEngine)
	{
		Guard.Against.Null(services);
		Guard.Against.NullOrWhiteSpace(databaseEngine);

		services
			.AddProSoftLibraryData(databaseEngine)
			.AddScoped<ITagManager, TagManager>()
			;

		return services;
	}

	public static IHost UseProSoftLibraryCore(this IHost host, ILogger logger, string databaseEngine)
	{
		Guard.Against.Null(host);
		Guard.Against.Null(logger);

		logger.LogInformation("Configure dependencies for: ProSoft.Library.Core");

		host
			.UseProSoftLibraryData(logger, databaseEngine)
			;

		return host;
	}

}