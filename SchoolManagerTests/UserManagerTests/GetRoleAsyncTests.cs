using Moq;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerTests.UserManagerTests;

public partial class UserManagerTests
{
    [TestMethod]
    public async Task GetRoleAsyncStudentTest()
    {
        _handler.Setup(m => m.GetRoleAsync(It.IsAny<User>()))
        .Returns(Task.FromResult<Role?>(null));
        Role role = await _userManager.GetRoleAsync(new User("test", "test", "test", "firstName", "lastName"));
        Assert.AreEqual(role, Role.Student);
    }

    [TestMethod]
    public async Task GetRoleAsyncTeacherTest()
    {
        _handler.Setup(m => m.GetRoleAsync(It.IsAny<User>()))
            .Returns(Task.FromResult<Role?>(Role.Teacher));
        Role role = await _userManager.GetRoleAsync(new User("test", "test", "test", "firstName", "lastName"));
        Assert.AreEqual(role, Role.Teacher);
    }

    [TestMethod]
    public async Task GetRoleAsyncAdministratorTest()
    {
        _handler.Setup(m => m.GetRoleAsync(It.IsAny<User>()))
            .Returns(Task.FromResult<Role?>(Role.Administrator));
        Role role = await _userManager.GetRoleAsync(new User("test", "test", "test", "firstName", "lastName"));
        Assert.AreEqual(role, Role.Administrator);
    }

}
