using Microsoft.EntityFrameworkCore;
using TestSMSHttpClientLibrary;

namespace TestSMSConsoleApp;
public class ApplicationContext : DbContext
{
    public DbSet<Dish> Dishes => Set<Dish>();
    private string _dbConnectionString;
    public ApplicationContext(string dbConnectionString)
    {
        _dbConnectionString = dbConnectionString;
        Database.EnsureCreated();
    }
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_dbConnectionString);
    }
}