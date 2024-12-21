using Microsoft.EntityFrameworkCore;
using SchoolManagerModel.Utils;

namespace SchoolManagerModel.Persistence;

public class SchoolDbContext : SchoolDbContextBase
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=school.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var seeder = new EntityDataSeeder(modelBuilder);
        seeder.SeedAllData();
    }
}