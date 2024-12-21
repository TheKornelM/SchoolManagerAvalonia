using SchoolManagerModel.Persistence;
using SchoolManagerModel.Utils;

namespace SchoolManagerModel.Managers;

public class LoginManager(IAsyncUserDataHandler schoolData)
{
    public async Task LoginAsync(string username, string password)
    {
        var result = await schoolData.GetUserAsync(username)
            ?? throw new Exception("User not found");


        string hashedPassword = HashStringMd5.GetHashedString(password);

        if (hashedPassword != result.Password)
        {
            throw new Exception("Invalid password!");
        }
    }


}