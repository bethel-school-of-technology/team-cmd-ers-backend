using Fit_Trac.Models;
using Microsoft.EntityFrameworkCore;

namespace Fit_Trac.Migrations;

public class GoalDbContext : DbContext
{
    public DbSet<Goal> Goal { get; set; }
    public DbSet<User> User { get; set; }

    public GoalDbContext(DbContextOptions<GoalDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Goal>(entity => 
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.Description);
            entity.Property(e => e.GoalToReach).IsRequired();
            entity.Property(e => e.UserProgress).IsRequired();
            entity.Property(e => e.DateCreated).IsRequired();

            entity.HasOne<User>(e => e.User)
                .WithMany(u => u.Goal)
                .HasForeignKey(e => e.UserId);
        });

        modelBuilder.Entity<User>(entity => 
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
            entity.HasIndex(x => x.Email).IsUnique();
            entity.Property(e => e.Password).IsRequired();

            entity.HasMany<Goal>(e => e.Goal)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserId);
        });
    }
}