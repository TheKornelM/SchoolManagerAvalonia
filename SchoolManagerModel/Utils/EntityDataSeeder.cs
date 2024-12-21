using Microsoft.EntityFrameworkCore;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Utils;

internal class EntityDataSeeder
{
    private ModelBuilder _modelBuilder;

    private User _adminUser;
    private User _teacherUser;
    private User _studentUser;

    private Teacher _teacher;
    private Student _student;

    private Class _class1;
    private Class _class2;

    private Subject _subject1;
    private Subject _subject2;
    private Subject _subject3;


    public EntityDataSeeder(ModelBuilder modelBuilder)
    {
        _modelBuilder = modelBuilder;
        _adminUser = new User("admin", HashStringMd5.GetHashedString("admin"), "admin@test.localhost", "firstName",
            "secondName")
        {
            Id = 1
        };

        _teacherUser = new User("teacher", HashStringMd5.GetHashedString("teacher"), "teacher@test.localhost", "Jakab",
           "Gipsz")
        {
            Id = 2
        };
        _teacher = new Teacher() { Id = 1, User = _teacherUser };

        _studentUser = new User("student", HashStringMd5.GetHashedString("student"), "student@test.localhost", "Béla",
           "Tóth")
        {
            Id = 3
        };

        _class1 = new Class() { Id = 1, Name = "1/B" };
        _class2 = new Class() { Id = 2, Name = "2/B" };

        _student = new Student() { Id = 1, Class = _class1, User = _studentUser };

        _subject1 = new Subject()
        {
            Id = 1,
            Name = "Matek1",
            Class = _class1,
            Teacher = _teacher
        };

        _subject2 = new Subject()
        {
            Id = 2,
            Name = "Történelem",
            Class = _class1,
            Teacher = _teacher
        };

        _subject3 = new Subject()
        {
            Id = 3,
            Name = "Testnevelés",
            Class = _class2,
            Teacher = _teacher
        };
    }

    public void SeedAllData()
    {
        AddAdmin();
        AddTeacher();
        AddStudent();
        AddClasses();
        AddSubjects();
        AddAssignedSubjects();
        AddMarks();
    }

    public void AddAdmin()
    {
        _modelBuilder.Entity<User>().HasData(_adminUser);

        // Assign administrator role
        var adminRole = new RoleRecord(1, 2)
        {
            Id = 1
        };
        _modelBuilder.Entity<RoleRecord>().HasData(adminRole);
    }

    public void AddTeacher()
    {
        _modelBuilder.Entity<User>().HasData(_teacherUser);

        var teacherRole = new RoleRecord(2, 1)
        {
            Id = 2
        };

        _modelBuilder.Entity<RoleRecord>().HasData(teacherRole);

        _modelBuilder.Entity<Teacher>().HasData(new
        {
            Id = 1,
            UserId = _teacherUser.Id
        });
    }

    public void AddClasses()
    {
        _modelBuilder.Entity<Class>().HasData(_class1);
        _modelBuilder.Entity<Class>().HasData(_class2);
    }

    /// <summary>
    /// Add a student
    /// </summary>
    /// <remarks>
    /// You need to execute AddClasses method, otherwise you will get foreign key error.
    /// </remarks>
    public void AddStudent()
    {
        _modelBuilder.Entity<User>().HasData(_studentUser);

        _modelBuilder.Entity<Student>().HasData(new
        {
            Id = _student.Id,
            UserId = _studentUser.Id,
            ClassId = _student.Class.Id
        });
    }

    public void AddSubjects()
    {
        _modelBuilder.Entity<Subject>().HasData(new
        {
            Id = 1,
            Name = _subject1.Name,
            ClassId = _subject1.Class.Id,
            TeacherId = _subject1.Teacher.Id
        });

        _modelBuilder.Entity<Subject>().HasData(new
        {
            Id = 2,
            Name = _subject2.Name,
            ClassId = _subject2.Class.Id,
            TeacherId = _subject2.Teacher.Id
        });

        _modelBuilder.Entity<Subject>().HasData(new
        {
            Id = 3,
            Name = _subject3.Name,
            ClassId = _subject3.Class.Id,
            TeacherId = _subject3.Teacher.Id
        });
    }

    public void AddAssignedSubjects()
    {
        _modelBuilder.Entity<AssignedSubject>().HasData(new
        {
            Id = 1,
            SubjectId = _subject1.Id,
            StudentId = _student.Id,
            GotGrade = false
        });

        _modelBuilder.Entity<AssignedSubject>().HasData(new
        {
            Id = 2,
            SubjectId = _subject2.Id,
            StudentId = _student.Id,
            GotGrade = false
        });
    }

    public void AddMarks()
    {
        _modelBuilder.Entity<Mark>().HasData(new
        {
            Id = 1,
            Grade = 5,
            StudentId = _student.Id,
            SubjectId = _subject1.Id,
            SubmitDate = DateTime.Now.AddDays(-1),
            Notes = "First mark"
        });

        _modelBuilder.Entity<Mark>().HasData(new
        {
            Id = 2,
            Grade = 3,
            StudentId = _student.Id,
            SubjectId = _subject1.Id,
            SubmitDate = DateTime.Now,
            Notes = "Second mark"
        });
    }
}
