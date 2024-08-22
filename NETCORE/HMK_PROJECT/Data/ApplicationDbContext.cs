using HMK_PROJECT.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HMK_PROJECT.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityApp>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<HeThongPhanPhoi> HTPP { get; set; }
    public DbSet<DaiLy> DaiLy { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<HeThongPhanPhoi>()
            .HasMany(e => e.DaiLy)
            .WithOne(p => p.HTPP)
            .HasForeignKey(c => c.MaHTPP)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Person>().ToTable("Person");
        modelBuilder.Entity<Employee>().ToTable("Employee");
    }
}
