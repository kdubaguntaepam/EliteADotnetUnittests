using AutomationFramwork.API.Core.Services;
using AutomationFramwork.API.Core.Models;
using NUnit.Framework;
using Moq;

namespace AutomationFramwork.API.Tests.UnitTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserService> _userServiceMock;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _userService = new UserService();
        }

        [Test]
        public void CreateUser_ShouldReturnUser_WhenValidInput()
        {
            // Arrange
            var user = new User { Id = 1, Name = "Test User" };
            _userServiceMock.Setup(s => s.CreateUser(It.IsAny<User>())).Returns(user);

            // Act
            var result = _userService.CreateUser(user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(user.Name, result.Name);
        }

        [Test]
        public void GetUserById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId, Name = "Test User" };
            _userServiceMock.Setup(s => s.GetUserById(userId)).Returns(user);

            // Act
            var result = _userService.GetUserById(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.Id);
            Assert.AreEqual(user.Name, result.Name);
        }
    }
}
