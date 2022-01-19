using System;
using JustAnotherToDo.Domain.Entities;
using JustAnotherToDo.Domain.Enums;
using JustAnotherToDo.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace JustAnotherToDo.Application.UnitTests.Common;
[SetUpFixture]
public class ApplicationContextFactory
{
    public static ApplicationDBContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var context = new ApplicationDBContext(options);
        context.Database.EnsureCreated();
        context.Profiles.AddRange(new[]
        {
            new UserProfile
            {
                Username = "Test",
                Password = "Test",
                AccessLevel = AccessLevel.Administrator
            },
            new UserProfile
            {
                Username = "Test2",
                Password = "Test2",
                AccessLevel = AccessLevel.User
            },
            new UserProfile()
            {
                UserId = JustAnotherToDoContextFactory.ProfileId,
                Username = "TestUser",
                Password = "TestUser",
                AccessLevel = AccessLevel.User
            }
        });
        context.SaveChanges();
        return context;
    }

    public static void Destroy(ApplicationDBContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}