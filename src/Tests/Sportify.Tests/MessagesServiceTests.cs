﻿namespace Sportify.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using Data.Models;
    using Data.ViewModels.Messages;
    using Data.ViewModels.Towns;
    using FakeManagers;
    using global::AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using Services;
    using Xunit;

    public class MessagesServiceTests
    {
        [Fact]
        public void IsSendMessageShouldReturnTrueUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase("Sportify_Database_Contacts_1")
                .Options;

            var context = new SportifyDbContext(options);
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperProfile()));
            var mapper = mapperConfig.CreateMapper();

            context.Users.Add(new User {UserName = "Gosho"});
            context.SaveChanges();

            var service = new MessagesService(context, new FakeUserManager(), mapper);
            var isSendMessage = service.IsSendMessage(new AddMessageViewModel { Username = "Gosho", Content = "Text"});
            
            Assert.True(isSendMessage);
        }

        [Fact]
        public void IsSendMessageShouldReturnFalseUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase("Sportify_Database_Contacts_2")
                .Options;

            var context = new SportifyDbContext(options);
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperProfile()));
            var mapper = mapperConfig.CreateMapper();

            var service = new MessagesService(context, new FakeUserManager(), mapper);
            var isSendMessage = service.IsSendMessage(new AddMessageViewModel { Username = "Pesho" });

            Assert.False(isSendMessage);
        }
    }
}
