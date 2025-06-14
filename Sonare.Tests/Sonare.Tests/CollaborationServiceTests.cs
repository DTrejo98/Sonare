using Moq;
using Xunit;
using Sonare.Services;
using Sonare.Models;
using Sonare.Interfaces;

namespace Sonare.Tests.Sonare.Tests
{
    public class CollaborationServiceTests
    {
        [Fact]
        public async Task GetAllCollaborations_ReturnsExpectedCollaborations()
        {
            // Arrange
            var expectedCollaborations = new List<Collaboration>
            {
                new Collaboration { Id = 1, OriginalClipId = 1, ResponseClipId = 2, CreatedAt = DateTime.UtcNow },
                new Collaboration { Id = 2, OriginalClipId = 3, ResponseClipId = 4, CreatedAt = DateTime.UtcNow }
            };

            var mockRepo = new Mock<ICollaborationRepository>();
            mockRepo.Setup(repo => repo.GetAllCollaborations())
                    .ReturnsAsync(expectedCollaborations);

            var service = new CollaborationService(mockRepo.Object);

            // Act
            var result = await service.GetAllCollaborations();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCollaborations.Count, result.Count);
            Assert.Collection(result,
                collab => Assert.Equal(1, collab.Id),
                collab => Assert.Equal(2, collab.Id));
        }
    }
}


