using DataManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataManager;
public class LotteryDbContext : DbContext
{
    private readonly string _connectionString;
    public LotteryDbContext(IConfiguration config)
    {
        var relativePath = config.GetConnectionString("LotteryDraws") ?? throw new NullReferenceException();
        _connectionString = Path.GetFullPath(relativePath);
    }

    public DbSet<LotteryDraw> Draws { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_connectionString}");
    }
}
