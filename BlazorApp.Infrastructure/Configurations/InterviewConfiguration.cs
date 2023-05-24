using BlazorApp.Bll.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorApp.Infrastructure.Configurations;

public class InterviewConfiguration : IEntityTypeConfiguration<Interview>
{
    public void Configure(EntityTypeBuilder<Interview> builder)
    {
        builder.ToTable("interviews");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).HasColumnName("Id");

        builder.Property(i => i.Date).HasColumnName("Date");

        builder.Property(i => i.Status).IsRequired();
        builder.Property(i => i.Note);

        builder.Property(i => i.VacancyId).HasColumnName("VacancyId").IsRequired();
        builder.HasOne<Vacancy>().WithMany().HasForeignKey(i => i.VacancyId);

        builder.Property(i => i.ResumeId).HasColumnName("ResumeId").IsRequired();
        builder.HasOne<Resume>().WithMany().HasForeignKey(i => i.ResumeId);
    }
}