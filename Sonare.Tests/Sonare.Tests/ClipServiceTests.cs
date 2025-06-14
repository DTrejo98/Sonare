using Moq;
using Xunit;
using Sonare.Interfaces;
using Sonare.Models;
using Sonare.Services;

namespace Sonare.Tests.Sonare.Tests
{
    public class ClipServiceTests
    {
        [Fact]
        public async Task GetAllClips_ReturnsExpectedClips()
        {
            // Arrange
            var expectedClips = new List<Clip>
            {
                new Clip
                {
                    Id = 1,
                    UserId = 1,
                    Title = "Clip A",
                    MediaUrl = "http://mediaurl.com/a.mp3",
                    CreatedAt = DateTime.UtcNow,
                    User = new User { Id = 1}
                },
                new Clip
                {
                    Id = 2,
                    UserId = 2,
                    Title = "Clip B",
                    MediaUrl = "http://mediaurl.com/b.mp3",
                    CreatedAt = DateTime.UtcNow,
                    User = new User { Id = 2}
                }
            };

            var mockRepo = new Mock<IClipRepository>();
            mockRepo.Setup(repo => repo.GetAllClips()).ReturnsAsync(expectedClips);

            var service = new ClipService(mockRepo.Object);

            // Act
            var result = await service.GetAllClips();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedClips.Count, result.Count);
            Assert.Collection(result,
                clip => Assert.Equal("Clip A", clip.Title),
                clip => Assert.Equal("Clip B", clip.Title));
        }
    }
}


