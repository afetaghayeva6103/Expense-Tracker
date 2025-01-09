using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExpenseTracker.Domain.Context;

public class AppDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }

    public AppDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            base.OnConfiguring(
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ProjectManagementConnectionString")));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Payment>().HasOne(x => x.Project).
            WithMany(x => x.Payments).HasForeignKey(x => x.ProjectId).
            OnDelete(DeleteBehavior.NoAction);
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<CurrencyRate> CurrencyRates { get; set; }
    public DbSet<GeneralExpense> GeneralExpenses { get; set; }
}
