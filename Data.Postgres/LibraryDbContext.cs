using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using ProSoft.Library.Core;
using ProSoft.Library.Core.Models;
using ProSoft.Library.Data.Postgres.Configurations;

namespace ProSoft.Library.Data.Postgres;

public class LibraryDbContext : DbContext
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		Guard.Against.Null(optionsBuilder);

		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable(EnvironmentVariables.ProSoftLibraryPostgresConnectionString));
		}

		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		Guard.Against.Null(modelBuilder);

		modelBuilder.Entity<Tag>().Configure();

		base.OnModelCreating(modelBuilder);
	}
}