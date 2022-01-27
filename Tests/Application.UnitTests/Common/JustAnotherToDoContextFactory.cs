using System;
using System.Linq;
using JustAnotherToDo.Domain.Entities;
using JustAnotherToDo.Domain.Enums;
using JustAnotherToDo.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace JustAnotherToDo.Application.UnitTests.Common;
[SetUpFixture]
public class JustAnotherToDoContextFactory
{
    public static Guid ProfileId = Guid.NewGuid();
    public static JustAnotherToDoDbContext Create()
    {
        var options = new DbContextOptionsBuilder<JustAnotherToDoDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var context = new JustAnotherToDoDbContext(options);
        context.Database.EnsureCreated();
        context.Categories.AttachRange(new []
        {
            new Category
            {
                Name = "Important",
                Color = "#FF0000",
                ProfileId = ProfileId
            },
            new Category
            {
                Name = "Not important",
                Color = "#00FF00",
                ProfileId = ProfileId,
            }
        });
        context.SaveChanges();
        context.ToDos.AddRange(new[]
        {
            new ToDo
            {
                Name = "Task1",
                CreationDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                CategoryId = context.Categories.First(c => c.ProfileId == ProfileId && c.Name == "Important").Id,
                ProfileId = ProfileId,
            },
            new ToDo
            {
                Name = "Task2",
                CreationDate = DateTime.Now,
                EndDate = null,
                CategoryId = context.Categories.First(c => c.ProfileId == ProfileId && c.Name == "Not important").Id,
                ProfileId = ProfileId,
            },
            new ToDo
            {
                Name = "Task3",
                CreationDate = DateTime.Now,
                EndDate = null,
                CategoryId = null,
                ProfileId = ProfileId
            }
        });
        context.SaveChanges();
        context.Profiles.AddRange(new UserProfile
        {
            Username = "Test",
            Password = "Test",
            AccessLevel = AccessLevel.Administrator
        }, new UserProfile
        {
            Username = "Test2",
            Password = "Test2",
            AccessLevel = AccessLevel.User
        }, new UserProfile()
        {
            UserId = ProfileId,
            Username = "TestUser",
            Password = "TestUser",
            AccessLevel = AccessLevel.User
        });
        context.SaveChanges();
        return context;
    }

    public static void Destroy(JustAnotherToDoDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}