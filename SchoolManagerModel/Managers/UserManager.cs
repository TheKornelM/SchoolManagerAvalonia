using SchoolManagerModel.DTOs;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Persistence;
using SchoolManagerModel.Utils;

namespace SchoolManagerModel.Managers;

public class UserManager(IAsyncUserDataHandler dataHandler)
{
    public async Task<bool> IsAdminAsync(User user)
    {
        return await GetRoleAsync(user) == Role.Administrator;
    }

    public async Task<Role> GetRoleAsync(User user)
    {

        /*
         * The student role is the default, so it is not stored
         * If there isn't an assigned role to the user, we handle the user as a student
         */
        Role? userRole = await dataHandler.GetRoleAsync(user);
        return userRole ?? Role.Student; // Return the role or default to Student
    }


    public async Task<bool> UserExistsAsync(User user)
    {
        return await dataHandler.UsernameExistsAsync(user.Username);
    }

    public async Task<bool> EmailAlreadyRegisteredAsync(string email)
    {
        return await dataHandler.EmailAlreadyRegisteredAsync(email);
    }

    public async Task AddStudentAsync(Student student)
    {
        await dataHandler.AddStudentAsync(student);
    }

    public async Task AddTeacherAsync(Teacher teacher)
    {
        await dataHandler.AddTeacherAsync(teacher);
    }

    public async Task AddAdminAsync(Admin admin)
    {
        await dataHandler.AddAdminAsync(admin);
    }

    public async Task AssignRoleAsync(User user, Role role)
    {
        if (!await UserExistsAsync(user))
        {
            throw new Exception("User not registered");
        }

        await dataHandler.AssignRoleAsync(user, role);
    }

    public async Task RegisterUserAsync(User user)
    {
        if (await UserExistsAsync(user))
        {
            throw new Exception("User already registered");
        }

        if (await EmailAlreadyRegisteredAsync(user.Email))
        {
            throw new Exception("Email already registered");
        }

        user.Password = HashStringMd5.GetHashedString(user.Password);

        await dataHandler.AddUserAsync(user);
    }


    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        if (!await UserExistsAsync(new User { Username = username }))
        {
            throw new Exception("User not found");
        }

        return await dataHandler.GetUserByUsernameAsync(username);
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        return await dataHandler.GetUsersAsync();
    }

    public async Task<Student> GetStudentByUserAsync(User user)
    {
        var result = await dataHandler.GetStudentByUserAsync(user);
        return result ?? throw new Exception("Student not found");
    }

    public async Task<List<UserDto>> FilterUsersAsync(string? username = null, string? firstName = null, string? lastName = null, string? email = null)
    {
        return await dataHandler.FilterUsersAsync(username, firstName, lastName, email);
    }
}

