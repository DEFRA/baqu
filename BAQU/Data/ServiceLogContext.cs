using Microsoft.EntityFrameworkCore;
using BAQU.Entities;

namespace BAQU.Data;

public class ServiceLogContext : DbContext
{
    public ServiceLogContext(DbContextOptions<ServiceLogContext> options) 
        : base(options)
    {
    }

    public DbSet<ServiceLogs> ServiceLog { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<ServiceLogs>().HasKey(e => e.Id);

        modelBuilder.Entity<ServiceLogs>().ToTable("WorkerServiceLog");
    }
}
