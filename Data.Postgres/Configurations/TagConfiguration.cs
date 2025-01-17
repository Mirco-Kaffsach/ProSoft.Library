using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSoft.Library.Core.Models;

namespace ProSoft.Library.Data.Postgres.Configurations;

internal static class TagConfiguration
{
    internal static void Configure(this EntityTypeBuilder<Tag> builder)
    {
        Guard.Against.Null(builder);

        builder.ToTable("Tags").HasKey(pk => pk.Id);
        
        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.SystemId).HasColumnName("SystemId").HasColumnType("uuid").IsRequired();
        builder.Property(p => p.Name).HasColumnName("Name").HasColumnType("character varying(128)").HasMaxLength(128).IsRequired();
        builder.Property(p => p.IsActive).HasColumnName("IsActive").HasColumnType("boolean").IsRequired();
    }
}
