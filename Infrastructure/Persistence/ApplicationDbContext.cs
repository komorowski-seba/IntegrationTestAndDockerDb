using Application;
using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        var carBuilder = builder.Entity<CarEntity>();
        carBuilder.HasKey(n => n.Id);
        carBuilder.Property(n => n.Id).IsRequired().ValueGeneratedNever();

        carBuilder.Property(n => n.Brand).HasMaxLength(500);
        carBuilder.Property(n => n.Name).HasMaxLength(500);
        carBuilder.Property(n => n.EngineCapacity).HasPrecision(3, 1);
        
        base.OnModelCreating(builder);
    }

    public DbSet<CarEntity> Cars { get; init; }
}