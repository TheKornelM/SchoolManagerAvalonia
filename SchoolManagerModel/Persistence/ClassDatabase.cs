using Microsoft.EntityFrameworkCore;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Persistence;

public class ClassDatabase(SchoolDbContext dbContext) : IAsyncClassDataHandler
{
    public async Task<List<Class>> GetClassesAsync()
    {
        return await dbContext.Classes
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddClassAsync(Class cls)
    {
        await dbContext.Classes.AddAsync(cls);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<User>> GetClassStudentsAsync(Class cls)
    {
        return await dbContext.Students
            .AsNoTracking()
            .Include(x => x.Class)
            .Where(x => x.Class.Id == cls.Id)
            .Select(x => x.User)
            .ToListAsync();
    }

    public async Task<List<Subject>> GetClassSubjectsAsync(Class cls)
    {
        return await dbContext.Subjects
            .AsNoTracking()
            .Include(x => x.Class)
            .Where(x => x.Class.Id == cls.Id)
            .ToListAsync();
    }

    public async Task<bool> ClassExistsAsync(Class cls)
    {
        return await dbContext.Classes
            .AsNoTracking()
            .AnyAsync(x => x.Name == cls.Name);
    }

    public async Task<Class?> GetClassByIdAsync(int classId)
    {
        return await dbContext.Classes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == classId);
    }

    public async Task DeleteClassAsync(Class cls)
    { 
        dbContext.Classes.Remove(cls);
        await dbContext.SaveChangesAsync();
    }
}
