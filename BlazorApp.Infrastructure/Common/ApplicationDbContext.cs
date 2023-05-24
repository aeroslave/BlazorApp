using Microsoft.EntityFrameworkCore;
using BlazorApp.Bll.Models;

namespace BlazorApp.Infrastructure.Common;

public class ApplicationDbContext : DbContext
{
    public DbSet<JobSeeker> JobSeekers { get; set; } = null!;

    public DbSet<Vacancy> Vacancies { get; set; } = null!;

    public DbSet<Resume> Resumes { get; set; } = null!;

    public DbSet<Interview> Interviews { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}