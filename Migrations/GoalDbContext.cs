using Fit_Trac.Models;
using Microsoft.EntityFrameworkCore;

namespace Fit_Trac.Migrations;

public class GoalDbContext : DbContext
{
    public DbSet<Goal> Goal { get; set; }

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
            entity.Property(e => e.GoalToReach).IsRequired();
            entity.Property(e => e.UserProgress).IsRequired();
            entity.Property(e => e.DateCreated);
        });
    }
}