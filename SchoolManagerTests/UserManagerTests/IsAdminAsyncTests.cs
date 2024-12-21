using Moq;
using SchoolManagerModel.Entities;

namespace SchoolManagerTests.UserManagerTests;

public partial class UserManagerTests
{
    [TestMethod]
    public async Task IsAdminAsync_ShouldReturnTrue_WhenUserIsAdmin()
    {
        _handler.Setup(dh => dh.GetRoleAsync(_testUser)).ReturnsAsync(Role.Administrator);

        var result = await _userManager.IsAdminAsync(_testUser);
        Assert.IsTrue(result);
        _handler.Verify(dh => dh.GetRoleAsync(_testUser), Times.Once);
    }

    [TestMethod]
    public async Task IsAdminAsync_ShouldReturnFalse_WhenUserIsNotAdmin()
    {
        _handler.Setup(dh => dh.GetRoleAsync(_testUser)).ReturnsAsync(Role.Student);

        var result = await _userManager.IsAdminAsync(_testUser);
        Assert.IsFalse(result);
    }
}