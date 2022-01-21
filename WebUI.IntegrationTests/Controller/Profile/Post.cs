using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Profiles.Commands.CreateProfile;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using SQLitePCL;
using WebUI.Controllers;
using Xunit;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Profile;

public class Post : IClassFixture<CustomWebApplicationFactory<ProfileController>>
{
    private readonly CustomWebApplicationFactory<ProfileController> _factory;

    public Post(CustomWebApplicationFactory<ProfileController> factory)
    {
        _factory = factory;
        
    }

    [Fact]
    public async Task ShouldPostNewProfile()
    {
        var client = await _factory.GetAuthenticatedClient();
        var profile = Utilities.GetRequestContent(new CreateProfileCommand
        {
            Username = "Newuser",
            Password = "12345"
        });
        var response = await client.PostAsync("api/Profile", profile);
        response.EnsureSuccessStatusCode();
    }
    [Fact]
    public async Task PostExistingUser()
    {
        var client = await _factory.GetAuthenticatedClient();
        var profile = new CreateProfileCommand
        {
            Username = "TestUser",
            Password = "TestUser"
        };
        var content = Utilities.GetRequestContent(profile);
        var response = await client.PostAsync("api/Profile", content);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    [Fact]
    public async Task AnonymousCanPost()
    {
        var client = _factory.CreateClient();
        var profile =  Utilities.GetRequestContent(new CreateProfileCommand
        {
            Username = "Newuser1",
            Password = "12345"
        });
        var response = await client.PostAsync("api/Profile", profile);
        response.EnsureSuccessStatusCode();
    }
}