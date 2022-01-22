using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using NUnit.Framework;
using WebUI.Controllers;


namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Category;

public class Delete 
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
    public async Task SuccessfullyDeletesCategory()
    {

        var response = await _client
               .DeleteAsync($"api/Category/{Utilities.CategoryId}");
        response.EnsureSuccessStatusCode();

    }
    [Test]
    public async Task NotFoundCategory()
    {
       
        var response = await _client
            .DeleteAsync($"api/Category/{Guid.NewGuid()}");
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

    }
    [Test]
    public async Task AnonymousCantDelete()
    {

        var response = await _factory.CreateClient()
            .DeleteAsync($"api/Category/{Utilities.CategoryId}");
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);

    }
}