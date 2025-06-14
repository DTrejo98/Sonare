using Moq;
using Xunit;
using Sonare;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sonare.Interfaces;
using Sonare.Models;
using Sonare.Services;

namespace Sonare.Tests.Sonare.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsExpectedUsers()
        {
            // Arrange
            var expectedUsers = new List<User>
            {
                new User { Id = 1},
                new User { Id = 2}
            };

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetAllUsers())
                    .ReturnsAsync(expectedUsers);  // Use ReturnsAsync for async methods

            var service = new UserService(mockRepo.Object);

            // Act
            var result = await service.GetAllUsers(); // Await the async method

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUsers.Count, result.Count);
        }
    }
}


