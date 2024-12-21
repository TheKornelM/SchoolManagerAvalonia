using Moq;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Exceptions;
using SchoolManagerModel.Managers;
using SchoolManagerModel.Persistence;

namespace SchoolManagerTests
{
    [TestClass]
    public class ClassManagerTests
    {
        private readonly Mock<IAsyncClassDataHandler> _mockDataHandler = new();
        private readonly ClassManager _classManager;

        private readonly Class _testClass = new() { Id = 1, Name = "1/A" };
        private readonly User _testStudent = new("username", "password", "email", "firstName", "lastName");
        private readonly Subject _testSubject;

        public ClassManagerTests()
        {
            // Mock the data handler dependency
            _classManager = new ClassManager(_mockDataHandler.Object);

            // Initialize test data
            _testSubject = new Subject
            {
                Id = 1,
                Name = "Math",
                Class = _testClass,
                Teacher = new Teacher { User = new User("teacher", "pass", "email", "firstName", "lastName") }
            };
        }
        
        [TestMethod]
        public async Task GetClassesAsync_ShouldReturnClasses()
        {
            var classes = new List<Class> { _testClass };
            _mockDataHandler.Setup(dh => dh.GetClassesAsync()).ReturnsAsync(classes);
            
            var result = await _classManager.GetClassesAsync();
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(_testClass, result[0]);
            _mockDataHandler.Verify(dh => dh.GetClassesAsync(), Times.Once);
        }

        [TestMethod]
        public async Task AddClassAsync_ShouldAddClass_WhenClassDoesNotExist()
        {
            _mockDataHandler.Setup(dh => dh.ClassExistsAsync(_testClass)).ReturnsAsync(false);
            await _classManager.AddClassAsync(_testClass);
            _mockDataHandler.Verify(dh => dh.AddClassAsync(_testClass), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ClassExistsException))]
        public async Task AddClassAsync_ShouldThrowException_WhenClassExists()
        {
            _mockDataHandler.Setup(dh => dh.ClassExistsAsync(_testClass)).ReturnsAsync(true);
            await _classManager.AddClassAsync(_testClass);
        }

        [TestMethod]
        public async Task GetClassStudentsAsync_ShouldReturnClassStudents()
        {
            var students = new List<User> { _testStudent };
            _mockDataHandler.Setup(dh => dh.GetClassStudentsAsync(_testClass)).ReturnsAsync(students);
            
            var result = await _classManager.GetClassStudentsAsync(_testClass);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(_testStudent, result[0]);
            _mockDataHandler.Verify(dh => dh.GetClassStudentsAsync(_testClass), Times.Once);
        }

        [TestMethod]
        public async Task GetClassSubjectsAsync_ShouldReturnClassSubjects()
        {
            var subjects = new List<Subject> { _testSubject };
            _mockDataHandler.Setup(dh => dh.GetClassSubjectsAsync(_testClass)).ReturnsAsync(subjects);
            
            var result = await _classManager.GetClassSubjectsAsync(_testClass);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(_testSubject, result[0]);
            _mockDataHandler.Verify(dh => dh.GetClassSubjectsAsync(_testClass), Times.Once);
        }
    }
}
