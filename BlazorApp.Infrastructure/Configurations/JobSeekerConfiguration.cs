using BlazorApp.Bll.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorApp.Infrastructure.Configurations;

public class JobSeekerConfiguration : IEntityTypeConfiguration<JobSeeker>
{
    public void Configure(EntityTypeBuilder<JobSeeker> builder)
    {
        builder.ToTable("job_seekers");
        builder.HasKey(j => j.Id);
        builder.Property(j => j.Id).HasColumnName("Id");
        builder.Property(j => j.FullName).HasMaxLength(100);

        builder.Property(j => j.Address).HasMaxLength(500);

        builder.Property(j => j.CellNumber).HasMaxLength(50);

        builder.Property(j => j.Email).HasMaxLength(50);

        builder.Property(j => j.UserId).HasColumnName("UserId");

        builder.HasOne<User>().WithOne().HasForeignKey<JobSeeker>(j => j.UserId);
    }
}