using Microsoft.EntityFrameworkCore;
using BAQU.Entities;

namespace BAQU.Data;

public class QualityCheckDBContext : DbContext
{
    public QualityCheckDBContext(DbContextOptions<QualityCheckDBContext> options) 
        : base(options)
    {
    }

    public DbSet<QualityCheckDetails> QualityCheckDetails { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<QualityCheck>()
            .ToTable("Check")
            .HasKey(e => e.CheckId);
        
        modelBuilder.Entity<QualityCheckDetails>()
            .ToTable("BankAccount");

        modelBuilder.Entity<QualityCheck>()
            .HasOne(q => q.QualityCheckDetails)
            .WithOne()
            .HasForeignKey<QualityCheckDetails>(q => q.CheckId);

    }
}
