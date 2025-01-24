using AutomationFramwork.API.Core.Interfaces;
using AutomationFramwork.API.Core.Models;
using AutomationFramwork.API.Core.Services;
using AutomationFramwork.API.Framework.ApiClients;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace AutomationFramwork.API.Tests.UnitTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserApiClient> _mockUserApiClient;
        private UserService _userService;

        [SetUp]
        public void SetUp()
        {
            _mockUserApiClient = new Mock<IUserApiClient>();
            _userService = new UserService(_mockUserApiClient.Object);
        }

        [Test]
        public async Task GetUserAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var expectedUser = new User { Id = userId, Name = "John Doe" };
            _mockUserApiClient.Setup(x => x.GetUserAsync(userId)).ReturnsAsync(expectedUser);

            // Act
            var result = await _userService.GetUserAsync(userId);

            // Assert
            Assert.That(result, Is.EqualTo(expectedUser));
        }

        [Test]
        public async Task CreateUserAsync_ShouldReturnCreatedUser_WhenUserIsCreated()
        {
            // Arrange
            var newUser = new User { Name = "Jane Doe" };
            var expectedUser = new User { Id = 1, Name = "Jane Doe" };
            _mockUserApiClient.Setup(x => x.CreateUserAsync(newUser)).ReturnsAsync(expectedUser);

            // Act
            var result = await _userService.CreateUserAsync(newUser);

            // Assert
            Assert.That(result, Is.EqualTo(expectedUser));
        }

        [Test]
        public async Task UpdateUserAsync_ShouldReturnUpdatedUser_WhenUserIsUpdated()
        {
            // Arrange
            var existingUser = new User { Id = 1, Name = "John Doe" };
            var updatedUser = new User { Id = 1, Name = "John Smith" };
            _mockUserApiClient.Setup(x => x.UpdateUserAsync(existingUser)).ReturnsAsync(updatedUser);

            // Act
            var result = await _userService.UpdateUserAsync(existingUser);

            // Assert
            Assert.That(result, Is.EqualTo(updatedUser));
        }

        [Test]
        public async Task DeleteUserAsync_ShouldReturnTrue_WhenUserIsDeleted()
        {
            // Arrange
            var userId = 1;
            _mockUserApiClient.Setup(x => x.DeleteUserAsync(userId)).ReturnsAsync(true);

            // Act
            var result = await _userService.DeleteUserAsync(userId);

            // Assert
            Assert.That(result, Is.True);
        }
    }
}