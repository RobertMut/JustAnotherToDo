using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Categories.Commands.UpdateCategory;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using NUnit.Framework;
using WebUI.Controllers;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Category;

public class Update
{
    private CustomWebApplicationFactory<CategoryController> _factory;
    private HttpClient _client;
    [SetUp]
    public async Task SetUp()
    {
        _factory = new CustomWebApplicationFactory<CategoryController>();
        _client = await _factory.GetAuthenticatedClient();

    }
    [Test]
    public async Task SuccessfullyUpdatesCategory()
    {
        var category = Utilities.GetRequestContent(new UpdateCategoryCommand
        {
            Id = Utilities.Category2Id,
            Name = "Not important",
            Color = "#00FF00"
        });
        var response = await _client
            .PutAsync($"api/Category", category);
        Console.WriteLine("TEST");
        response.EnsureSuccessStatusCode();

    }
    [Test]
    public async Task WrongIdCategoryUpdate()
    {
        var category = new UpdateCategoryCommand
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Color = "#FFFF00"
        };
        var content = Utilities.GetRequestContent(category);
        var response = await _client
            .PutAsync($"api/Category", content);
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

    }
    [Test]
    public async Task AnonymousCantUpdate()
    {
        var category = Utilities.GetRequestContent(new UpdateCategoryCommand
        {
            Id = Utilities.CategoryId,
            Name = "Not important",
            Color = "#00FF00"
        });
        var client = _factory.CreateClient();
        var response = await client
            .PutAsync($"api/Category", category);
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}