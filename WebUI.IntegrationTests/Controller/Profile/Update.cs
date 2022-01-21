using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Profiles.Commands.UpdateProfile;
using JustAnotherToDo.Domain.Enums;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using WebUI.Controllers;
using Xunit;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Profile;

public class Update : IClassFixture<CustomWebApplicationFactory<ProfileController>>
{
    private CustomWebApplicationFactory<ProfileController> _factory;
    private HttpClient _client;
    private readonly StringContent _profile;

    public Update(CustomWebApplicationFactory<ProfileController> factory)
    {
        _factory = factory;
        _client = _factory.GetAuthenticatedClient().Result;
        
        _profile = Utilities.GetRequestContent(new UpdateProfileCommand
        {
            UserId = Utilities.TestUserId,
            Username = "TestUser",
            Password = "NewPass",
            AccessLevel = AccessLevel.Administrator
        });
    }
    [Fact]
    public async Task SuccessfullyUpdatesProfile()
    {
        var response = await _client
            .PutAsync($"api/Profile", _profile);
        response.EnsureSuccessStatusCode();

    }
    [Fact]
    public async Task WrongIdProfileUpdate()
    {
        var profile = new UpdateProfileCommand
        {
            UserId = Guid.NewGuid(),
            Username = "Test2",
            Password = "Test2",
            AccessLevel = AccessLevel.User
        };
        var content = Utilities.GetRequestContent(profile);
        var response = await _client
            .PutAsync($"api/Profile", content);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

    }
    [Fact]
    public async Task AnonymousCantUpdate()
    {
        var client = _factory.CreateClient();
        var response = await client
            .PutAsync($"api/Profile", _profile);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}