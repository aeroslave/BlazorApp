using BlazorApp.Bll.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorApp.Infrastructure.Configurations;

public class ResumeConfiguration : IEntityTypeConfiguration<Resume>
{
    public void Configure(EntityTypeBuilder<Resume> builder)
    {
        builder.ToTable("resumes");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnName("Id");

        builder.Property(r => r.Description);

        builder.Property(i => i.UserId).HasColumnName("UserId").IsRequired();
        builder.HasOne<JobSeeker>().WithMany().HasForeignKey(r => r.UserId);
    }
}