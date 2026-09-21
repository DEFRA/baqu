using Microsoft.EntityFrameworkCore;
using BAQU.Entities;

namespace BAQU.Data;

public class PeopleContext : DbContext
{
    public PeopleContext(DbContextOptions<PeopleContext> options)
        : base(options)
    {
    }

    public DbSet<Person> Person { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 

        modelBuilder.Entity<Person>().HasKey(e => e.PersonId);
    }
}
