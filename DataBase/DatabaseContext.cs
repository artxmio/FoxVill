using FoxVill.Model;
using Microsoft.EntityFrameworkCore;

namespace FoxVill.DataBase;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlite("Data Source=database.db");
    }
}
