namespace Sportify.Tests
{
    using System.Linq;

    using Data;
    using Data.Models;
    using Data.ViewModels.Messages;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Xunit;

    public class MessagesServiceTest : BaseServiceTests
    {
        [Fact]
        public void SendMessage_ShouldReturnCorrectMessage()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new MessagesService(context, this.Mapper, null, null);

            context.Users.Add(new User {Id = "testId", UserName = "George"});
            context.SaveChanges();

            // Act
            var message = service.Send(new SendMessageViewModel
            {
                FullName = "George",
                Subject = "Subject",
                Content = "Text"
            }, context.Users.FirstOrDefault());

            // Expected Message
            var expectedMessage = new Message
            {
                FullName = "George",
                Subject = "Subject",
                Content = "Text",
                UserId = "testId"
            };

            // Assert
            Assert.True(message.Equals(expectedMessage));
        }

        [Fact]
        public void GetAllMessages_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new MessagesService(context, this.Mapper, null, null);

            context.Add(new Message());
            context.Add(new Message());
            context.SaveChanges();

            // Act
            var result = service.GetAllMessages().Count();

            // Assert
            Assert.Equal(2, result);
        }
    }
}
