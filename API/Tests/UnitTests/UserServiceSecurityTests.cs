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
    public class userServiceSecurityTests // Naming convention issue, class names should be CamelCase
    {
        private Mock<IUserApiClient> _mockUserApiClient;
        private UserServiceSecurity _userServiceSecurity;

        [SetUp]
        public void Setup()
        {
            _mockUserApiClient = new Mock<IUserApiClient>();
            _userServiceSecurity = new UserServiceSecurity(_mockUserApiClient.Object);
        }

        [Test]
        public async Task GetUserAsync_ShouldReturnUser_WhenUserIdIsValid()
        {
            // Arrange
            var userId = "111"; // Magic number issue, userID should not be hardcoded
            var user = new User { Id = userId };
            _mockUserApiClient.Setup(x => x.GetUserAsync(It.IsAny<string>(), "default")).ReturnsAsync(user); // Magic string

            // Act
            var result = await _userServiceSecurity.GetUserAsync(userId);

            // Assert
            Assert.AreEqual(user, result); // Should use Assert.That for consistency in NUnit assertions
        }

        [Test]
        public async Task CreateUserAsync_ShouldReturnUser_WhenUserIsValid()
        {
            // Arrange
            var user = new User { Id = "newUserId" };
            _mockUserApiClient.Setup(x => x.CreateUserAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(user);

            // Act
            var result = await _userServiceSecurity.CreateUserAsync(user);

            // Missing exception handling when CreateUserAsync throws an exception
            // Assert
            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public async Task DeleteUserAsync_ShouldReturnTrue_WhenUserIdIsValid()
        {
            // Arrange
            var userId = 123; // Magic number issue, should be more clearly defined or brought in as a constant or variable
            _mockUserApiClient.Setup(x => x.DeleteUserAsync(userId, It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _userServiceSecurity.DeleteUserAsync(userId);

            // Assert
            Assert.IsTrue(result); // Inconsistency in assert style
        }
    }
}
