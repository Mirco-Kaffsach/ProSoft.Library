using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProSoft.Library.Data.Infrastructure;

namespace ProSoft.Library.Core.Infrastructure;

public static class DependencyResolver
{
	public static IServiceCollection AddProSoftLibraryCore(this IServiceCollection services)
	{
		Guard.Against.Null(services);

		services
			.AddProSoftLibraryData()
			;

		return services;
	}

	public static IHost UseProSoftLibraryCore(this IHost host, ILogger logger)
	{
		Guard.Against.Null(host);
		Guard.Against.Null(logger);

		logger.LogInformation("Configure dependencies for: ProSoft.Library.Core");

		host
			.UseProSoftLibraryData(logger)
			;

		return host;
	}

}