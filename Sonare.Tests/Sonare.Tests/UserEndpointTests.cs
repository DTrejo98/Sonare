using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using Sonare.Endpoints;
using Sonare.Interfaces;
using Sonare.Models;

namespace Sonare.Tests.Sonare.Tests
{
    public class UserEndpointTests
    {
        private readonly HttpClient _client;

        public UserEndpointTests()
        {
            var host = new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder.UseTestServer();
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddRouting();
                        services.AddScoped<IUserService, DummyUserService>();
                    });
                    webBuilder.Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapUserEndpoints();
                        });
                    });
                })
                .Start();

            _client = host.GetTestClient();
        }

        [Fact]
        public async Task GetUsers_ReturnsSuccess()
        {
            var response = await _client.GetAsync("/api/users");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateUser_ReturnsSuccess()
        {
            var newUser = new User { Username = "TestUser" };
            var response = await _client.PostAsJsonAsync("/api/users", newUser);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateUser_ReturnsSuccess()
        {
            var updatedUser = new User { Id = 1, Username = "UpdatedUser" };
            var response = await _client.PutAsJsonAsync("/api/users/1", updatedUser);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_ReturnsSuccess()
        {
            var response = await _client.DeleteAsync("/api/users/1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }

    // Dummy user service implementation
    public class DummyUserService : IUserService
    {
        public Task<List<User>> GetAllUsers() =>
            Task.FromResult(new List<User> { new User { Id = 1, Username = "MockUser" } });

        public Task<User?> GetUserById(int id) =>
            Task.FromResult<User?>(new User { Id = id, Username = "MockUser" });

        public Task<User?> CreateUser(User user)
        {
            user.Id = 99; // Simulate DB assigned ID
            return Task.FromResult<User?>(user);
        }

        public Task<User?> UpdateUser(int id, User user)
        {
            user.Id = id;
            return Task.FromResult<User?>(user);
        }

        public Task<User> DeleteUser(int id) =>
            Task.FromResult(new User { Id = id, Username = "DeletedUser" });
    }
}

