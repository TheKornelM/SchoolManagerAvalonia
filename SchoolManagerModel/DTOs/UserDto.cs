using SchoolManagerModel.Utils;

namespace SchoolManagerModel.DTOs;
public class UserDto
{
    public string Username { get; set; }
    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Name => CultureUtils.GetFullName(FirstName, LastName);

    public UserDto()
    {

    }

    public UserDto(string username, string email, string firstName, string lastName)
    {
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
}
