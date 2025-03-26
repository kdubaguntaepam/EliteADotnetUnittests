using AutomationFramwork.API.Framework.ApiClients;
using AutomationFramwork.API.Core.Models;
using NUnit.Framework;
using Moq;
using System.Threading.Tasks;

namespace AutomationFramwork.API.Tests.UnitTests
{
    [TestFixture]
    public class UserApiClientTests
    {
        private Mock<UserApiClient> _userApiClientMock;
        private UserApiClient _userApiClient;

        [SetUp]
        public void Setup()
        {
            _userApiClientMock = new Mock<UserApiClient>();
            _userApiClient = new UserApiClient();
        }

        [Test]
        public async Task GetUser_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId, Name = "Test User" };
            _userApiClientMock.Setup(c => c.GetUser(userId)).ReturnsAsync(user);

            // Act
            var result = await _userApiClient.GetUser(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.Id);
            Assert.AreEqual(user.Name, result.Name);
        }
    }
}
