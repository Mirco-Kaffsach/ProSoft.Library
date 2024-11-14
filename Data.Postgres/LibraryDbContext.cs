using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using ProSoft.Library.Core;

namespace ProSoft.Library.Data.Postgres;

public class LibraryDbContext : DbContext
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		Guard.Against.Null(optionsBuilder);

		if (optionsBuilder.IsConfigured == false)
		{
			optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable(EnvironmentVariables.ProSoftLibraryPostgresConnectionString));
		}

		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		Guard.Against.Null(modelBuilder);

		//modelBuilder.Entity<DataType>().ConfigureEntity();

		base.OnModelCreating(modelBuilder);
	}
}