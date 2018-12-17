namespace Sportify.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data;
    using Data.Models;
    using Data.ViewModels.Messages;
    using global::AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
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

            // Act
            var message = service.SendMessage(new AddMessageViewModel {Name = "George", Content = "Text" }, null);

            // Expected Message
            var expectedMessage = new Message
            {
                Name = "George",
                Content = "Text"
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
