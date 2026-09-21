using BAQU.Entities;
using Microsoft.EntityFrameworkCore;

namespace BAQU.Data;

public class DAXContext : DbContext
{
    public DAXContext(DbContextOptions<DAXContext> options) 
        : base(options)
    {
    }

    public virtual DbSet<RSFDwhVendVendorBankAccountStaging> DAXVendBankAccount { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 
        
        modelBuilder.Entity<RSFDwhVendVendorBankAccountStaging>(e => 
        {
            e.ToTable("RSFDwhVendVendorBankAccountStaging")
            .HasKey(e => new { e.VENDACCOUNT, e.MODIFIEDDATETIME });

            e.Property(e => e.ADDRESSLATITUDE)
                .HasPrecision(32, 10);
            e.Property(e => e.ADDRESSLONGITUDE)
                .HasPrecision(32, 10);
            e.Property(e => e.CROSSRATE)
                .HasPrecision(32, 16);

        });
    }
}
