using ProSoft.Library.Api.Infrastructure;
using ProSoft.Library.Core;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Ardalis.GuardClauses;

[assembly: InternalsVisibleTo("ProSoft.Library.Api.UnitTests")]

#if DEBUG
	Environment.SetEnvironmentVariable("PROSOFT_LIBRARY_DATABASE_ENGINE", "postgres");
	Environment.SetEnvironmentVariable("PROSOFT_LIBRARY_POSTGRES_CONNECTIONSTRING", "Host=10.215.10.50;Database=prosoft-library-dev;Username=postgres;Password=Geheim123#;");
#endif

var databaseEngine = Environment.GetEnvironmentVariable(EnvironmentVariables.ProSoftLibraryDatabaseEngine);
#if DEBUG
	databaseEngine = "postgres";
#else
	Guard.Against.NullOrWhiteSpace(databaseEngine, message: $"No environment variable with name '{EnvironmentVariables.ProSoftLibraryDatabaseEngine}' found");
#endif

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
       .AddProSoftLibrary(databaseEngine)
       .AddControllers()
       .AddJsonOptions
       (
	       options =>
	       {
			   options.JsonSerializerOptions.PropertyNamingPolicy = null;
	       }
       );

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
	.AddOpenApi();

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseProSoftLibrary(logger, databaseEngine);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
