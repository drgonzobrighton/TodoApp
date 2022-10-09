using Microsoft.EntityFrameworkCore;
using TodoApp.DataAccess.DomainModels;

namespace TodoApp.DataAccess;
public class TodoContext : DbContext
{
    public DbSet<DomainTodoItem> TodoItems { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost;Database=TodoDb;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DomainTodoItem>(t =>
        {
            t.ToTable("TodoItems");
            t.HasKey(x => x.Id);
            t.Property(x => x.Description).HasMaxLength(256);

            t.HasIndex(t => t.CompleteBy)
                .IncludeProperties(p => new { p.Description, p.IsComplete, p.Id });
        });
    }
}
