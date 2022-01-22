using System.Net;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Profiles.Queries.GetProfileDetail;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using NUnit.Framework;
using WebUI.Controllers;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Profile;

public class GetProfile
{
    private CustomWebApplicationFactory<ProfileController> _factory;
    [SetUp]
    public void SetUp()
    {
        _factory = new CustomWebApplicationFactory<ProfileController>();

    }

    [Test]
    public async Task ReturnsLoggedUser()
    {
        var client = await _factory.GetAuthenticatedClient();
        var response = await client.GetAsync("api/Profile/profile");
        response.EnsureSuccessStatusCode();
        var vm = await Utilities.GetResponseContent<ProfileDetailVm>(response);
        Assert.IsInstanceOf<ProfileDetailVm>(vm);
        Assert.NotNull(vm.Username);
    }
    //Deleted
    //[Test]
    //public async Task GetNonExistingUser()
    //{
    //    var client = await _factory.GetAuthenticatedClient();
    //    var response = await client.GetAsync("api/Profile/profile?NonExistingUser");
    //    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    //}
    [Test]
    public async Task AnonymousCantGet()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("api/Profile/profile");
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}