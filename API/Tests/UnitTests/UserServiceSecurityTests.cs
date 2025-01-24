using System;
using System.Threading.Tasks;
using AutomationFramwork.API.Core.Models;
using AutomationFramwork.API.Core.Services;
using AutomationFramwork.API.Framework.ApiClients;
using Moq;
using NUnit.Framework;

namespace AutomationFramwork.API.Tests.UnitTests
{
    [TestFixture]
    public class UserServiceSecurityTests
    {
        private Mock<IUserApiClient> _mockUserApiClient;
        private UserServiceSecurity _userServiceSecurity;

        [SetUp]
        public void SetUp()
        {
            _mockUserApiClient = new Mock<IUserApiClient>();
            _userServiceSecurity = new UserServiceSecurity(_mockUserApiClient.Object);
        }

        [Test]
        public async Task GetUserAsync_ShouldReturnUser_WhenUserIdIsValid()
        {
            // Arrange
            var userId = "validUserId";
            var user = new User { Id = userId };
            _mockUserApiClient.Setup(x => x.GetUserAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(user);

            // Act
            var result = await _userServiceSecurity.GetUserAsync(userId);

            // Assert
            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public async Task CreateUserAsync_ShouldReturnUser_WhenUserIsValid()
        {
            // Arrange
            var user = new User { Id = "newUserId" };
            _mockUserApiClient.Setup(x => x.CreateUserAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(user);

            // Act
            var result = await _userServiceSecurity.CreateUserAsync(user);

            // Assert
            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public async Task UpdateUserAsync_ShouldReturnUpdatedUser_WhenUserIsValid()
        {
            // Arrange
            var user = new User { Id = "existingUserId" };
            _mockUserApiClient.Setup(x => x.UpdateUserAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(user);

            // Act
            var result = await _userServiceSecurity.UpdateUserAsync(user);

            // Assert
            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public async Task DeleteUserAsync_ShouldReturnTrue_WhenUserIdIsValid()
        {
            // Arrange
            var userId = 1;
            _mockUserApiClient.Setup(x => x.DeleteUserAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _userServiceSecurity.DeleteUserAsync(userId);

            // Assert
            Assert.That(result, Is.True);
        }
    }
}