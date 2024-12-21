using Moq;

namespace SchoolManagerTests.UserManagerTests;

public partial class UserManagerTests
{
    [TestMethod]
    public async Task GetUserByUsername_ShouldThrowException_WhenUserNotFound()
    {
        _handler.Setup(dh => dh.UsernameExistsAsync(_testUser.Username)).ReturnsAsync(false);
        await Assert.ThrowsExceptionAsync<Exception>(() => _userManager.GetUserByUsernameAsync(_testUser.Username));
    }

    [TestMethod]
    public async Task GetUserByUsername_ShouldReturnUser_WhenUserExists()
    {
        _handler.Setup(dh => dh.UsernameExistsAsync(_testUser.Username)).ReturnsAsync(true);
        _handler.Setup(dh => dh.GetUserByUsernameAsync(_testUser.Username)).ReturnsAsync(_testUser);

        var result = await _userManager.GetUserByUsernameAsync(_testUser.Username);
        Assert.AreEqual(_testUser, result);
    }
}