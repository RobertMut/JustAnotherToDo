using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using NUnit.Framework;
using WebUI.Controllers;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Profile;

public class Delete
{
    private CustomWebApplicationFactory<ProfileController> _factory;
    private HttpClient _client;
    [SetUp]
    public async Task SetUp()
    {
        _factory = new CustomWebApplicationFactory<ProfileController>();
        _client = await _factory.GetAuthenticatedClient();
    }

    [Test]
    public async Task SuccessfullyDeletesProfile()
    {

        var response = await _client
               .DeleteAsync($"api/Profile/{Utilities.Test2Id}");
        response.EnsureSuccessStatusCode();

    }
    [Test]
    public async Task NotFoundProfile()
    {
       
        var response = await _client
            .DeleteAsync($"api/Profile/{Guid.NewGuid()}");
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

    }
    [Test]
    public async Task AnonymousCantDelete()
    {
        var response = await _factory.CreateClient()
            .DeleteAsync($"api/Profile/{Guid.NewGuid()}");
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}