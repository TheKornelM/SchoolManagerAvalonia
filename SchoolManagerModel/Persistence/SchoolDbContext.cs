using Microsoft.EntityFrameworkCore;
using SchoolManagerModel.Utils;
using System.Runtime.InteropServices;

namespace SchoolManagerModel.Persistence;

public class SchoolDbContext : SchoolDbContextBase
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string databasePath = "school.db";

        if (RuntimeInformation.RuntimeIdentifier.StartsWith("android"))
        {
            databasePath = Path.Combine(
             Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "school.db");
        }

        optionsBuilder.UseSqlite($"Data Source={databasePath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var seeder = new EntityDataSeeder(modelBuilder);
        seeder.SeedAllData();
    }
}