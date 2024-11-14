using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProSoft.Library.Core.Infrastructure;

namespace ProSoft.Library.Api.Infrastructure;

public static class DependencyResolver
{
	public static IServiceCollection AddProSoftLibrary(this IServiceCollection services)
	{
		Guard.Against.Null(services);

		services
			.AddProSoftLibraryCore()
			;

		return services;
	}

	public static IHost UseProSoftLibrary(this IHost host, ILogger logger)
	{
		Guard.Against.Null(host);
		Guard.Against.Null(logger);

		logger.LogInformation("Configuring dependencies for: ProSoft.Library.Api");

		host
			.UseProSoftLibraryCore(logger)
			;

		return host;
	}
}
