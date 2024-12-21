using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Persistence;

namespace SchoolManagerModel.Managers;

public class TeacherManager(IAsyncTeacherDataHandler dataHandler)
{
    public async Task<List<Teacher>> GetTeacherUsersAsync()
    {
        return await dataHandler.GetTeachersAsync();
    }

    public async Task<List<Subject>> GetCurrentTaughtSubjectsAsync(Teacher teacher)
    {
        return await dataHandler.GetCurrentTaughtSubjectsAsync(teacher);
    }
}
