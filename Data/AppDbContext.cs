using api.Models.Contacts;
using Microsoft.EntityFrameworkCore;

namespace api.Data;
public class AppDbContext : DbContext
{
    public DbSet<Contact>? Mensagens { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        var connectionString = "Server=sql;User ID=sa;Password=Banc19o87;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True";
        optionsBuilder.UseSqlServer(connectionString);
        
        base.OnConfiguring(optionsBuilder);
    }
}

