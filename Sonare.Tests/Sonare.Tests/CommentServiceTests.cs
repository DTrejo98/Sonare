using Moq;
using Xunit;
using Sonare.Services;
using Sonare.Models;
using Sonare.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sonare.Tests.Sonare.Tests
{
    public class CommentServiceTests
    {
        [Fact]
        public async Task GetAllComments_ReturnsExpectedComments()
        {
            // Arrange
            var expectedComments = new List<Comment>
            {
                new Comment { Id = 1, ClipId = 1, Body = "Nice clip!" },
                new Comment { Id = 2, ClipId = 2, Body = "Great!" }
            };

            var mockRepo = new Mock<ICommentRepository>();
            mockRepo.Setup(repo => repo.GetAllComments())
                    .ReturnsAsync(expectedComments);

            var service = new CommentService(mockRepo.Object);

            // Act
            var result = await service.GetAllComments();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedComments.Count, result.Count);
            Assert.Contains(result, c => c.Body == "Nice clip!");
            Assert.Contains(result, c => c.Body == "Great!");
        }
    }
}
