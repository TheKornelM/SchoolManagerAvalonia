using Microsoft.EntityFrameworkCore;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Persistence;

public class TeacherDatabase(SchoolDbContextBase dbContext) : IAsyncTeacherDataHandler
{
    public async Task<List<User>> GetSubjectStudentsAsync(Subject subject)
    {
        var result = await dbContext.AssignedSubjects
            .Where(x => x.Subject.Id == subject.Id)
            .Select(x => x.Subject.Id)
            .ToListAsync();

        var students = await dbContext.Users
            .Where(x => result.Contains(x.Id))
            .ToListAsync();

        return students;
    }

    public async Task<List<Subject>> GetCurrentTaughtSubjectsAsync(Teacher teacher)
    {
        var result = await dbContext.Subjects
            .Include(x => x.Teacher).ThenInclude(x => x.User)
            .Where(x => x.Teacher.User.Id == teacher.User.Id)
            .ToListAsync();
        return result;
    }

    public async Task<List<Class>> GetCurrentTaughtClassesAsync(Teacher teacher)
    {
        return await dbContext.Subjects
            .Include(x => x.Teacher)
            .Where(x => x.Teacher.Id == teacher.Id)
            .Select(x => x.Class)
            .ToListAsync();
    }

    public async Task<List<Teacher>> GetTeachersAsync()
    {
        return await dbContext.Teachers
            .Include(x => x.User)
            .ToListAsync();
    }

    public async Task<Teacher?> GetTeacherByIdAsync(int teacherId)
    {
        return await dbContext.Teachers.FirstOrDefaultAsync(x => x.Id == teacherId);
    }

    public async Task<List<Subject>> GetAssignedSubjectsAsync(User user)
    {
        return await dbContext.Subjects
            .Include(x => x.Teacher).ThenInclude(x => x.User)
            .Where(x => x.Teacher.User.Id == user.Id)
            .ToListAsync();
    }
}