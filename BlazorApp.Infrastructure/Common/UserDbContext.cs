using BlazorApp.Bll.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Infrastructure.Common;

public class UserDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
    }
}