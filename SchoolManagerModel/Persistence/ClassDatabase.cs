using Microsoft.EntityFrameworkCore;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Persistence;

public class ClassDatabase(SchoolDbContextBase dbContext) : IAsyncClassDataHandler
{
    public async Task<List<Class>> GetClassesAsync()
    {
        return await dbContext.Classes.ToListAsync();
    }

    public async Task AddClassAsync(Class cls)
    {
        await dbContext.Classes.AddAsync(cls);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<User>> GetClassStudentsAsync(Class cls)
    {
        return await dbContext.Students
            .Include(x => x.Class)
            .Where(x => x.Class.Id == cls.Id)
            .Include(x => x.User)
            .Select(x => x.User)
            .ToListAsync();
    }

    public async Task<List<Subject>> GetClassSubjectsAsync(Class cls)
    {
        return await dbContext.Subjects
            .Include(x => x.Class)
            .Where(x => x.Class.Id == cls.Id)
            .ToListAsync();
    }

    public async Task<bool> ClassExistsAsync(Class cls)
    {
        return await dbContext.Classes
            .AnyAsync(x => x.Name == cls.Name);
    }
}