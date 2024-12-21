using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Persistence;

public interface IAsyncTeacherDataHandler
{
    public Task<List<User>> GetSubjectStudentsAsync(Subject subject);
    public Task<List<Class>> GetCurrentTaughtClassesAsync(Teacher user);
    public Task<List<Teacher>> GetTeachersAsync();
    public Task<Teacher?> GetTeacherByIdAsync(int teacherId);

    public Task<List<Subject>> GetCurrentTaughtSubjectsAsync(Teacher teacher);

}
