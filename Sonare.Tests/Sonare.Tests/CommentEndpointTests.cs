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
using System;
using System.Collections.Generic;

namespace Sonare.Tests.Sonare.Tests
{
    public class CommentEndpointTests
    {
        private readonly HttpClient _client;

        public CommentEndpointTests()
        {
            var commentServiceMock = new Mock<ICommentService>();

            commentServiceMock.Setup(s => s.GetAllComments())
                .ReturnsAsync(new List<Comment>
                {
                    new Comment
                    {
                        Id = 1,
                        Body = "Test comment",
                        ClipId = 1,
                        CreatedAt = DateTime.UtcNow
                    }
                });

            commentServiceMock.Setup(s => s.CreateComment(It.IsAny<Comment>()))
                .ReturnsAsync((Comment comment) =>
                {
                    comment.Id = 99; // simulate database id assignment
                    return comment;
                });

            // Mock for UpdateComment returns the updated comment
            commentServiceMock.Setup(s => s.UpdateComment(It.IsAny<int>(), It.IsAny<Comment>()))
                .ReturnsAsync((int id, Comment comment) =>
                {
                    comment.Id = id;
                    return comment;
                });

            // Mock for DeleteComment returns a deleted comment (or could be the comment before deletion)
            commentServiceMock.Setup(s => s.DeleteComment(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                    new Comment
                    {
                        Id = id,
                        Body = "Deleted comment",
                        ClipId = 1,
                        CreatedAt = DateTime.UtcNow
                    });

            var host = new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder.UseTestServer();
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddRouting();
                        services.AddScoped<ICommentService>(_ => commentServiceMock.Object);
                    });
                    webBuilder.Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapCommentEndpoints(); // Your CommentEndpoints extension method
                        });
                    });
                })
                .Start();

            _client = host.GetTestClient();
        }

        [Fact]
        public async Task GetComments_ReturnsSuccess()
        {
            var response = await _client.GetAsync("/api/comments");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateComment_ReturnsSuccess()
        {
            var newComment = new Comment
            {
                Body = "Test comment",
                ClipId = 1,
                CreatedAt = DateTime.UtcNow
            };

            var response = await _client.PostAsJsonAsync("/api/comments", newComment);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateComment_ReturnsSuccess()
        {
            var updatedComment = new Comment
            {
                Id = 1,
                Body = "Updated comment",
                ClipId = 1,
                CreatedAt = DateTime.UtcNow
            };

            var response = await _client.PutAsJsonAsync("/api/comments/1", updatedComment);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteComment_ReturnsSuccess()
        {
            var response = await _client.DeleteAsync("/api/comments/1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

