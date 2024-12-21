using Moq;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerTests.UserManagerTests;

public partial class UserManagerTests
{
    [TestMethod]
    [ExpectedException(typeof(Exception), "User not registered")]
    public async Task UserNotRegisteredTests()
    {
        SetupUsernameExistsAsync(false);
        await _userManager.AssignRoleAsync(new User("test", "test", "test", "firstName", "lastName"), Role.Student);
    }

    [TestMethod]
    public async Task RegisteredUserTest()
    {
        List<RoleRecord> roles = [];
        var user = new User("test", "test", "test", "firstName", "lastName") { Id = 1 };

        SetupUsernameExistsAsync(true);
        _handler.Setup(m => m.AssignRoleAsync(It.IsAny<User>(), It.IsAny<Role>())).Callback(() =>
        {
            // Assign teacher role
            roles.Add(new RoleRecord(1, 1));
        });

        await _userManager.AssignRoleAsync(user, Role.Teacher);

        _handler.Setup(m => m.GetRoleAsync(It.IsAny<User>()))
            .Returns(Task.FromResult((Role?)roles
            .FirstOrDefault(roleRecord => roleRecord.UserId == user.Id)?.RoleId));

        var assignedRole = await _userManager.GetRoleAsync(user);
        Assert.AreEqual(Role.Teacher, assignedRole);

    }
    
    
    [TestMethod]
    public async Task GetRoleAsync_ShouldReturnStudent_WhenRoleIsNull()
    {
        _handler.Setup(dh => dh.GetRoleAsync(_testUser)).ReturnsAsync((Role?)null);

        var result = await _userManager.GetRoleAsync(_testUser);
        Assert.AreEqual(Role.Student, result);
    }

    [TestMethod]
    public async Task AssignRoleAsync_ShouldThrowException_WhenUserNotFound()
    {
        _handler.Setup(dh => dh.UsernameExistsAsync(_testUser.Username)).ReturnsAsync(false);
        await Assert.ThrowsExceptionAsync<Exception>(() => _userManager.AssignRoleAsync(_testUser, Role.Teacher));
    }

    [TestMethod]
    public async Task AssignRoleAsync_ShouldAssignRole_WhenUserExists()
    {
        _handler.Setup(dh => dh.UsernameExistsAsync(_testUser.Username)).ReturnsAsync(true);

        await _userManager.AssignRoleAsync(_testUser, Role.Teacher);
        _handler.Verify(dh => dh.AssignRoleAsync(_testUser, Role.Teacher), Times.Once);
    }

    private void SetupUsernameExistsAsync(bool returnValue)
    {
        _handler.Setup(m => m.UsernameExistsAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(returnValue));
    }


}
