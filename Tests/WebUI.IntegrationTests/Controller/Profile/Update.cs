using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Profiles.Commands.UpdateProfile;
using JustAnotherToDo.Domain.Enums;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using NUnit.Framework;
using WebUI.Controllers;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Profile;

public class Update
{
    private CustomWebApplicationFactory<ProfileController> _factory;
    private HttpClient _client;
    private StringContent _profile;

    [SetUp]
    public async Task SetUp()
    {
        _factory = new CustomWebApplicationFactory<ProfileController>();
        _client = await _factory.GetAuthenticatedClient();
        _profile = Utilities.GetRequestContent(new UpdateProfileCommand
        {
            UserId = Utilities.TestUserId,
            Username = "TestUser",
            Password = "NewPass",
            AccessLevel = AccessLevel.Administrator
        });
    }
    [Test]
    public async Task SuccessfullyUpdatesProfile()
    {
        var response = await _client
            .PutAsync($"api/Profile", _profile);
        response.EnsureSuccessStatusCode();

    }
    [Test]
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
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

    }
    [Test]
    public async Task AnonymousCantUpdate()
    {
        var client = _factory.CreateClient();
        var response = await client
            .PutAsync($"api/Profile", _profile);
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}