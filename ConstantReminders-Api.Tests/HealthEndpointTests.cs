using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ConstantReminders_Api.Tests
{
    public class HealthEndpointTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {
        [Fact]
        public async Task HealthEndpoint_WhenCalled_ShouldReturnsHealthyStatus()
        {
            // Arrange
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("/health");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<HealthResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(result);
            Assert.Equal("healthy", result.Status, ignoreCase: true);
        }

        [Theory]
        [InlineData("/health")]
        [InlineData("/HEALTH")]
        [InlineData("/Health")]
        public async Task HealthEndpoint_IsCaseInsensitive(string endpoint)
        {
            // Arrange
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync(endpoint);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }

    public class HealthResponse
    {
        public string Status { get; set; }
    }
}