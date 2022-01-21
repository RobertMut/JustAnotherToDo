using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using WebUI.Controllers;
using Xunit;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Profile;

public class Delete : IClassFixture<CustomWebApplicationFactory<ProfileController>>
{
    private CustomWebApplicationFactory<ProfileController> _factory;
    private HttpClient _client;
    public Delete(CustomWebApplicationFactory<ProfileController> factory)
    {
        _factory = factory;
        _client = _factory.GetAuthenticatedClient().Result;
    }

    [Fact]
    public async Task SuccessfullyDeletesProfile()
    {

        var response = await _client
               .DeleteAsync($"api/Profile/{Utilities.Test2Id}");
        response.EnsureSuccessStatusCode();

    }
    [Fact]
    public async Task NotFoundProfile()
    {
       
        var response = await _client
            .DeleteAsync($"api/Profile/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

    }
    [Fact]
    public async Task AnonymousCantDelete()
    {
        var response = await _factory.CreateClient()
            .DeleteAsync($"api/Profile/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}