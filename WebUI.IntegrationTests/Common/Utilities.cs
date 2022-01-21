using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JustAnotherToDo.Domain.Entities;
using JustAnotherToDo.Domain.Enums;
using JustAnotherToDo.Infrastructure.Identity;
using JustAnotherToDo.Persistence;
using Newtonsoft.Json;

namespace JustAnotherToDo.WebUI.IntegrationTests.Common;

public static class Utilities
{
    public static Guid CategoryId { get; } = Guid.NewGuid();
    public static Guid Category2Id { get; } = Guid.NewGuid();
    public static Guid TestUserId { get; } = Guid.NewGuid();
    public static Guid Test2Id { get; } = Guid.NewGuid();
    public static Guid ToDoId { get; } = Guid.NewGuid();
    public static Guid ToDo2Id { get; } = Guid.NewGuid();

    public static StringContent GetRequestContent(object obj)
    {
        return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
    }

    public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
    {
        var stringResponse = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<T>(stringResponse);
        return result;
    }

    public static void InitializeDbForTests(JustAnotherToDoDbContext context)
    {
        context.ToDos.AddRange(new ToDo
        {
            Id = ToDoId,
            Name = "Test2",
            CreationDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(5),
            CategoryId = Category2Id,
            ProfileId = TestUserId
        },
            new ToDo
            {
                Id = ToDo2Id,
                Name = "Test",
                CreationDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                CategoryId = CategoryId,
                ProfileId = Test2Id
            });
        context.Categories.AddRange(new Category
        {
            Id = Category2Id,
            Name = "Important",
            Color = "#FF0000",
            ProfileId = Test2Id
        }, new Category
        {
            Id = CategoryId,
            Name = "TestCategory",
            Color = "#FF00FF",
            ProfileId = TestUserId
        });
        context.SaveChanges();
    }



    public static void InitializeApplicationDbForTests(ApplicationDBContext context)
    {
        context.Profiles.AddRange(new UserProfile
        {
            UserId = TestUserId,
            Username = "TestUser",
            Password = "TestUser",
            AccessLevel = AccessLevel.Administrator
        },
            new UserProfile
            {
                UserId = Test2Id,
                Username = "Test2",
                Password = "Test2",
                AccessLevel = AccessLevel.User
            });
        context.SaveChanges();

    }
}