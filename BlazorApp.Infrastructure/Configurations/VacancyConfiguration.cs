using BlazorApp.Bll.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorApp.Infrastructure.Configurations;

public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
{
    public void Configure(EntityTypeBuilder<Vacancy> builder)
    {
        builder.ToTable("vacancies");
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).HasColumnName("Id");

        builder.Property(v => v.Title)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        builder.Property(v => v.IsActive)
            .IsRequired();

        builder.Property(v => v.Description);
    }
}