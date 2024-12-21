using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Persistence;

namespace SchoolManagerModel.Managers;

public class SubjectManager(IAsyncSubjectDataHandler dataHandler)
{
    public async Task AddSubjectMarkAsync(Mark mark)
    {
        await CheckAssignedSubjectToStudentAsync(mark.Student, mark.Subject);
        await dataHandler.AddMarkAsync(mark);
    }

    public async Task<List<Mark>> GetStudentSubjectMarksAsync(Student student, Subject subject)
    {
        await CheckAssignedSubjectToStudentAsync(student, subject);
        return await dataHandler.GetStudentSubjectMarksAsync(student, subject);
    }


    private async Task CheckAssignedSubjectToStudentAsync(Student student, Subject subject)
    {
        if (!await dataHandler.IsAssignedSubjectToStudentAsync(student.User, subject))
            throw new Exception("Subject is not assigned to student");
    }

    public async Task AddSubjectAsync(Subject subject)
    {
        await dataHandler.AddSubjectAsync(subject);
    }

    public async Task AssignSubjectsToStudentAsync(Student student, List<Subject> subjects)
    {
        await dataHandler.AssignSubjectsToStudentAsync(student, subjects);
    }

    public async Task<List<Student>> GetSubjectStudentsAsync(Subject subject)
    {
        return await dataHandler.GetSubjectStudentsAsync(subject);
    }

    public async Task<List<Mark>> GetStudentMarksAsync(Student student)
    {
        return await dataHandler.GetStudentMarksAsync(student);
    }
}