using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Xunit;
using Sonare.Endpoints;
using Sonare.Interfaces;
using Sonare.Models;

namespace Sonare.Tests.Sonare.Tests
{
    public class ClipEndpointTests
    {
        private readonly HttpClient _client;

        public ClipEndpointTests()
        {
            var clipServiceMock = new Mock<IClipService>();

            clipServiceMock.Setup(s => s.GetAllClips())
                .ReturnsAsync(new List<Clip> {
                    new Clip { Id = 1, Title = "Test Clip", UserId = 1, MediaUrl = "http://test.com", CreatedAt = DateTime.UtcNow }
                });

            clipServiceMock.Setup(s => s.CreateClip(It.IsAny<Clip>()))
                .ReturnsAsync((Clip clip) => new Clip
                {
                    Id = 99,
                    Title = clip.Title,
                    UserId = clip.UserId,
                    MediaUrl = clip.MediaUrl,
                    CreatedAt = clip.CreatedAt
                });

            // Return a Clip object for UpdateClip
            clipServiceMock.Setup(s => s.UpdateClip(It.IsAny<int>(), It.IsAny<Clip>()))
                .ReturnsAsync((int id, Clip clip) =>
                {
                    clip.Id = id;
                    return clip;
                });

            // Return a Clip object for DeleteClip (maybe the deleted clip)
            clipServiceMock.Setup(s => s.DeleteClip(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                    new Clip { Id = id, Title = "Deleted Clip", UserId = 1, MediaUrl = "http://deleted.com", CreatedAt = DateTime.UtcNow });

            var host = new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder.UseTestServer();
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddRouting();
                        services.AddScoped<IClipService>(_ => clipServiceMock.Object);
                    });
                    webBuilder.Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapClipEndpoints();
                        });
                    });
                })
                .Start();

            _client = host.GetTestClient();
        }

        [Fact]
        public async Task GetClips_ReturnsSuccess()
        {
            var response = await _client.GetAsync("/api/clips");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateClip_ReturnsSuccess()
        {
            var newClip = new Clip
            {
                Title = "Test Clip",
                UserId = 1,
                MediaUrl = "http://testmedia.com/clip.mp3",
                CreatedAt = DateTime.UtcNow
            };

            var response = await _client.PostAsJsonAsync("/api/clips", newClip);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateClip_ReturnsSuccess()
        {
            var updatedClip = new Clip
            {
                Id = 1,
                Title = "Updated Clip",
                UserId = 1,
                MediaUrl = "http://updated.com/clip.mp3",
                CreatedAt = DateTime.UtcNow
            };

            var response = await _client.PutAsJsonAsync("/api/clips/1", updatedClip);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteClip_ReturnsSuccess()
        {
            var response = await _client.DeleteAsync("/api/clips/1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}





