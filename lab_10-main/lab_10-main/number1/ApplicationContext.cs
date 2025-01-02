using Microsoft.EntityFrameworkCore;
using TicketsLibrary;

public class ApplicationContext : DbContext
{
    public DbSet<Ticker> Tickers { get; set; } = null!;
    public DbSet<Price> Prices { get; set; } = null!;
    public DbSet<TodaysCondition> TodaysConditions { get; set; } = null!;
    
    public ApplicationContext() 
    {
        Database.EnsureCreatedAsync();
    }
    //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestDB;Trusted_Connection=True;MultipleActiveResultSets=True");
    //optionsBuilder.UseSqlite("Data Source=C:/Users/rybal/OneDrive/Рабочий стол/tickers.sql");
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestDB;Trusted_Connection=True;MultipleActiveResultSets=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Ticker>().HasKey(u => new {u.id, u.name });
        modelBuilder.Entity<Price>().HasOne(p => p.ticker).WithMany(q => q.Prices)
            .HasForeignKey(r => new { r.tickerId, r.tickerName });
        modelBuilder.Entity<TodaysCondition>().HasOne(p => p.ticker).WithOne(q => q.Condition)
            .HasForeignKey<TodaysCondition>(e => new { e.tickerId, e.tickerName });
    }
}