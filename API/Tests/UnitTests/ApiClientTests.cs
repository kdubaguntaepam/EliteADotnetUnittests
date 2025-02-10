using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AutomationFramwork.API.Tests.UnitTests
{
    public class ApiClientTests
    {
        [Fact]
        public async Task GetAsync_ShouldReturnSuccessStatusCode()
        {
            // Arrange
            var httpClient = new HttpClient();
            var apiClient = new BaseApiClient(httpClient);

            // Act
            var response = await apiClient.GetAsync("https://api.example.com/resource");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task PostAsync_ShouldReturnSuccessStatusCode()
        {
            // Arrange
            var httpClient = new HttpClient();
            var apiClient = new BaseApiClient(httpClient);
            var content = new StringContent("{\"name\":\"test\"}", Encoding.UTF8, "application/json");

            // Act
            var response = await apiClient.PostAsync("https://api.example.com/resource", content);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
