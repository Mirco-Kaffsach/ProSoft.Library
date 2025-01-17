using ProSoft.Library.Core;
using ProSoft.Library.Core.Exceptions;

namespace ProSoft.Library.Data.Postgres;

internal static class DatabaseConfig
{
	internal static string ConnectionString =>
		Environment.GetEnvironmentVariable(EnvironmentVariables.ProSoftLibraryPostgresConnectionString)
		??
#if DEBUG
		"Host=10.215.10.50;Database=prosoft-library-dev;Username=postgres;Password=Geheim123#;";
#else
		throw new EnvironmentVariableNotFoundException(EnvironmentVariables.ProSoftLibraryPostgresConnectionString);
#endif
}