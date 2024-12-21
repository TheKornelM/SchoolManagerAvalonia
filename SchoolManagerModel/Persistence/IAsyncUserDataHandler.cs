using SchoolManagerModel.DTOs;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Persistence;

public interface IAsyncUserDataHandler
{
    public Task<User?> GetUserAsync(string username);
    public Task<Role?> GetRoleAsync(User user);
    public Task<bool> UsernameExistsAsync(string username);
    public Task<User?> GetUserByUsernameAsync(string username);
    public Task<bool> EmailAlreadyRegisteredAsync(string email);
    public Task AssignRoleAsync(User user, Role role);
    public Task AddUserAsync(User user);
    public Task<List<UserDto>> GetUsersAsync();
    public Task AddStudentAsync(Student student);
    public Task AddTeacherAsync(Teacher teacher);
    public Task AddAdminAsync(Admin admin);
    public Task<Student?> GetStudentByUserAsync(User user);
    public Task<List<UserDto>> FilterUsersAsync(string? username = null, string? firstName = null, string? lastName = null, string? email = null);

}
