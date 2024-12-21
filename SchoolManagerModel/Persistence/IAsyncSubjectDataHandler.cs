using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Persistence;

public interface IAsyncSubjectDataHandler
{
    public Task<List<Mark>> GetStudentSubjectMarksAsync(Student student, Subject subject);
    public Task<bool> IsAssignedSubjectToStudentAsync(User student, Subject subject);
    public Task AddMarkAsync(Mark mark);
    public Task AddSubjectAsync(Subject subject);
    public Task AssignSubjectsToStudentAsync(Student student, List<Subject> subjects);
    public Task<List<Student>> GetSubjectStudentsAsync(Subject subject);
    public Task<List<Mark>> GetStudentMarksAsync(Student student);
}