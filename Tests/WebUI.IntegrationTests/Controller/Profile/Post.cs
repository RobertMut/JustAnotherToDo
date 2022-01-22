using System.Net;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Profiles.Commands.CreateProfile;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using NUnit.Framework;
using WebUI.Controllers;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Profile;

public class Post
{
    private CustomWebApplicationFactory<ProfileController> _factory;

    [SetUp]
    public void SetUp()
    {
        _factory = new CustomWebApplicationFactory<ProfileController>();

    }

    [Test]
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
    [Test]
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
        Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
    }
    [Test]
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