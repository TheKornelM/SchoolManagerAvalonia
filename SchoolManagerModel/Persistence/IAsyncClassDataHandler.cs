using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Persistence;

public interface IAsyncClassDataHandler
{
    public Task<List<Class>> GetClassesAsync();
    public Task AddClassAsync(Class cls);
    public Task<List<User>> GetClassStudentsAsync(Class cls);
    public Task<List<Subject>> GetClassSubjectsAsync(Class cls);
    public Task<bool> ClassExistsAsync(Class cls);
    public Task<Class?> GetClassByIdAsync(int classId);
}
