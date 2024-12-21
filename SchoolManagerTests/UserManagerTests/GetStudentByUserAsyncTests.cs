using Moq;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerTests.UserManagerTests;

public partial class UserManagerTests
{
    [TestMethod]
    public async Task GetStudentByUser_ShouldThrowException_WhenStudentNotFound()
    {
        _handler.Setup(dh => dh.GetStudentByUserAsync(_testUser)).ReturnsAsync((Student?)null);
        await Assert.ThrowsExceptionAsync<Exception>(() => _userManager.GetStudentByUserAsync(_testUser));
    }

    [TestMethod]
    public async Task GetStudentByUser_ShouldReturnStudent_WhenStudentExists()
    {
        _handler.Setup(dh => dh.GetStudentByUserAsync(_testUser)).ReturnsAsync(_testStudent);

        var result = await _userManager.GetStudentByUserAsync(_testUser);
        Assert.AreEqual(_testStudent, result);
    }

}