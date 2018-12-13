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
    using Sportify.Tests.FakeManagers;
    using Xunit;

    public class MessagesServiceTest : BaseServiceTests
    {
        [Fact]
        public void IsSendMessage_ShouldReturnTrue()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            var result = userManager.CreateAsync(new User { UserName = "George" }, "1234").GetAwaiter().GetResult();
            var service = new MessagesService(context, this.Mapper, userManager, null);

            // Act
            var isSendMessage = service.IsSendMessage(new AddMessageViewModel { Username = "George", Content = "Text" });

            // Assert
            Assert.True(isSendMessage);
        }

        [Fact]
        public void IsSendMessage_ShouldReturnFalse()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            var result = userManager.CreateAsync(new User { UserName = "George" }, "1234").GetAwaiter().GetResult();
            var service = new MessagesService(context, this.Mapper, userManager, null);

            // Act
            var isSendMessage = service.IsSendMessage(new AddMessageViewModel { Username = "Peter", Content = "Text" });

            // Assert
            Assert.False(isSendMessage);
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
