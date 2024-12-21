using Moq;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerTests.UserManagerTests;

public partial class UserManagerTests
{
    [TestMethod]
    [ExpectedException(typeof(Exception), "User already registered")]
    public async Task UserAlreadyRegisteredTest()
    {
        var user = new User("test", "test", "test", "firstName", "lastName");
        _handler.Setup(m => m.UsernameExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
        await _userManager.RegisterUserAsync(user);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Email already registered")]
    public async Task EmailAlreadyRegisteredTest()
    {
        var user = new User("test", "test", "test", "firstName", "lastName");
        _handler.Setup(m => m.UsernameExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
        _handler.Setup(m => m.EmailAlreadyRegisteredAsync(It.IsAny<string>())).ReturnsAsync(true);
        await _userManager.RegisterUserAsync(user);
    }

    [TestMethod]
    public async Task SuccessfulRegistration()
    {
        List<User> users = [];
        var user = new User("test", "test", "test", "firstName", "lastName");

        _handler.Setup(m => m.AddUserAsync(It.IsAny<User>())).Callback(() => users.Add(user));
        await _userManager.RegisterUserAsync(user);

        _handler.Setup(m => m.UsernameExistsAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(users.Contains(user)));
        Assert.IsTrue(await _userManager.UserExistsAsync(user));
    }
    
    
    [TestMethod]
    public async Task RegisterUserAsync_ShouldThrowException_WhenUserAlreadyExists()
    {
        _handler.Setup(dh => dh.UsernameExistsAsync(_testUser.Username)).ReturnsAsync(true);
        await Assert.ThrowsExceptionAsync<Exception>(() => _userManager.RegisterUserAsync(_testUser));
    }

    [TestMethod]
    public async Task RegisterUserAsync_ShouldAddUser_WhenUserIsNew()
    {
        _handler.Setup(dh => dh.UsernameExistsAsync(_testUser.Username)).ReturnsAsync(false);
        _handler.Setup(dh => dh.EmailAlreadyRegisteredAsync(_testUser.Email)).ReturnsAsync(false);

        await _userManager.RegisterUserAsync(_testUser);
        _handler.Verify(dh => dh.AddUserAsync(It.Is<User>(u => u.Username == _testUser.Username && u.Password != "password")), Times.Once);
    }

}
