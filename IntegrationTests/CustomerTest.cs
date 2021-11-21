using AutoFixture;
using GlobalBlue.Api;
using GlobalBlue.Dtos;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class BasicTests
    : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly Fixture _fixture;
        private readonly HttpClient _client;

        public BasicTests(CustomWebApplicationFactory<Startup> factory)
        {
            _fixture = new Fixture();
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("/api/Customer/List")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task ShouldCreateCustomer()
        {
            // Arrange          
            var customer = _fixture.Build<CustomerDto>()
                  .With(c => c.Email, _fixture.Create<MailAddress>().ToString())
                  .Create();


            // Act
            var response = await _client.PostAsJsonAsync("/api/Customer/Create", customer);

            // Assert
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<CustomerDto>();

            Assert.NotEqual(0, created.Id);
        }
        [Fact]
        public async Task ShouldCreateUpdateAndDeleteCustomer()
        {
            // Arrange
            var customer = _fixture.Build<CustomerDto>()
                 .With(c => c.Email, _fixture.Create<MailAddress>().ToString())
                 .Create();

            // Act
            var response = await _client.PostAsJsonAsync("/api/Customer/Create", customer);

            // Assert
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<CustomerDto>();

            // Act

            created.FirstName = _fixture.Create<string>();
            response = await _client.PutAsJsonAsync("/api/Customer/Update", created);

            // Assert
            response.EnsureSuccessStatusCode();



            response = await _client.GetAsync($"/api/Customer/Get?id={created.Id}");
            response.EnsureSuccessStatusCode();

            var updated = await response.Content.ReadFromJsonAsync<CustomerDto>();
            Assert.Equal(updated.FirstName, created.FirstName);

            response = await _client.DeleteAsync($"/api/Customer?id={created.Id}");
            response.EnsureSuccessStatusCode();

            response = await _client.GetAsync($"/api/Customer/Get?id={created.Id}");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}