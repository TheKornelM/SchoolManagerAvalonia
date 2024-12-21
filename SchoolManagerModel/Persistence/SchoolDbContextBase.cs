using Microsoft.EntityFrameworkCore;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Persistence;

public abstract class SchoolDbContextBase : DbContext
{
    public DbSet<RoleRecord> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<AssignedSubject> AssignedSubjects { get; set; }
    public DbSet<Mark> Marks { get; set; }

    protected abstract override void OnConfiguring(DbContextOptionsBuilder optionsBuilder);

    protected abstract override void OnModelCreating(ModelBuilder modelBuilder);
}