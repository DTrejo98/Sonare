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
    public class CollaborationEndpointTests
    {
        private readonly HttpClient _client;

        public CollaborationEndpointTests()
        {
            var collaborationServiceMock = new Mock<ICollaborationService>();

            collaborationServiceMock.Setup(s => s.GetAllCollaborations())
                .ReturnsAsync(new List<Collaboration>
                {
                    new Collaboration
                    {
                        Id = 1,
                        OriginalClipId = 1,
                        ResponseClipId = 2,
                        CreatedAt = DateTime.UtcNow
                    }
                });

            collaborationServiceMock.Setup(s => s.CreateCollaboration(It.IsAny<Collaboration>()))
                .ReturnsAsync((Collaboration collab) =>
                {
                    collab.Id = 99; // simulate database ID
                    return collab;
                });

            // Return updated Collaboration for UpdateCollaboration
            collaborationServiceMock.Setup(s => s.UpdateCollaboration(It.IsAny<int>(), It.IsAny<Collaboration>()))
                .ReturnsAsync((int id, Collaboration collab) =>
                {
                    collab.Id = id;
                    return collab;
                });

            // Return deleted Collaboration for DeleteCollaboration
            collaborationServiceMock.Setup(s => s.DeleteCollaboration(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                    new Collaboration
                    {
                        Id = id,
                        OriginalClipId = 1,
                        ResponseClipId = 2,
                        CreatedAt = DateTime.UtcNow
                    });

            var host = new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder.UseTestServer();
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddRouting();
                        services.AddScoped<ICollaborationService>(_ => collaborationServiceMock.Object);
                    });
                    webBuilder.Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapCollaborationEndpoints();
                        });
                    });
                })
                .Start();

            _client = host.GetTestClient();
        }

        [Fact]
        public async Task GetCollaborations_ReturnsSuccess()
        {
            var response = await _client.GetAsync("/api/collaborations");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateCollaboration_ReturnsSuccess()
        {
            var newCollab = new Collaboration
            {
                OriginalClipId = 1,
                ResponseClipId = 2,
                CreatedAt = DateTime.UtcNow
            };

            var response = await _client.PostAsJsonAsync("/api/collaborations", newCollab);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateCollaboration_ReturnsSuccess()
        {
            var updatedCollab = new Collaboration
            {
                Id = 1,
                OriginalClipId = 3,
                ResponseClipId = 4,
                CreatedAt = DateTime.UtcNow
            };

            var response = await _client.PutAsJsonAsync("/api/collaborations/1", updatedCollab);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteCollaboration_ReturnsSuccess()
        {
            var response = await _client.DeleteAsync("/api/collaborations/1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

