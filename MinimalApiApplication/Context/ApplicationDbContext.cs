using Microsoft.EntityFrameworkCore;
using MinimalApiApplication.Entities;
namespace MinimalApiApplication.Context;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Todo>().HasQueryFilter(t => !t.IsDeleted); //todo: change it to contain all entities that inherit form base entity
    }

    public DbSet<Todo> Todos { get; set; }
}
