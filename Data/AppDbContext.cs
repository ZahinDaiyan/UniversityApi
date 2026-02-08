using Microsoft.EntityFrameworkCore;
using UniversityApi.Models;
namespace UniversityApi.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  public DbSet<University> Universities { get; set; }
  public DbSet<Student> Students { get; set; }

  public DbSet<User> Users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // University Configuration
    modelBuilder.Entity<University>(entity =>
    {
      entity.HasKey(u => u.Id);

      entity.Property(u => u.Name)
        .IsRequired()
        .HasMaxLength(100);

      entity.Property(u => u.CityName)
        .IsRequired()
        .HasMaxLength(100);

      entity.HasMany(u => u.Students)
        .WithOne(s => s.University)
        .HasForeignKey(s => s.UniversityId)
        .OnDelete(DeleteBehavior.Cascade);
    });

    // Student Configuration
    modelBuilder.Entity<Student>(entity =>
    {
      entity.HasKey(s => s.Id);

      entity.Property(s => s.Name)
        .IsRequired()
        .HasMaxLength(100);

      entity.Property(s => s.Age)
        .IsRequired();

    });

    // User Configuration
    modelBuilder.Entity<User>(entity =>
    {
      entity.HasKey(u => u.Id);

      entity.Property(u => u.Username)
        .IsRequired()
        .HasMaxLength(100);

    });

  }
}