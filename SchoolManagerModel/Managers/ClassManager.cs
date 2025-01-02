using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Exceptions;
using SchoolManagerModel.Persistence;

namespace SchoolManagerModel.Managers;

public class ClassManager(IAsyncClassDataHandler dataHandler)
{
    public async Task<List<Class>> GetClassesAsync()
    {
        return await dataHandler.GetClassesAsync();
    }

    public async Task AddClassAsync(Class cls)
    {
        if (await dataHandler.ClassExistsAsync(cls))
        {
            throw new ClassExistsException($"{cls.Name} class exists");
        }

        await dataHandler.AddClassAsync(cls);
    }

    public async Task<List<User>> GetClassStudentsAsync(Class cls)
    {
        return await dataHandler.GetClassStudentsAsync(cls);
    }

    public async Task<List<Subject>> GetClassSubjectsAsync(Class cls)
    {
        return await dataHandler.GetClassSubjectsAsync(cls);
    }

    public async Task<Class?> GetClassByIdAsync(int classId)
    {
        return await dataHandler.GetClassByIdAsync(classId);
    }

    public async Task DeleteClassAsync(Class cls)
    {
        var studentsTask = dataHandler.GetClassStudentsAsync(cls);
        var subjectsTask = dataHandler.GetClassSubjectsAsync(cls);
        
        await Task.WhenAll(studentsTask, subjectsTask);

        if (studentsTask.Result.Count != 0)
        {
            throw new Exception("Cannot delete class which contains students");
        }

        if (subjectsTask.Result.Count != 0)
        {
            throw new Exception("Cannot delete class which contains subjects");
        }

        await dataHandler.DeleteClassAsync(cls);
    }

}
