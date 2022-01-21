using System.Net;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Profiles.Queries.GetProfileDetail;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using WebUI.Controllers;
using Xunit;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Profile;

public class GetProfile : IClassFixture<CustomWebApplicationFactory<ProfileController>>
{
    private CustomWebApplicationFactory<ProfileController> _factory;
    public GetProfile(CustomWebApplicationFactory<ProfileController> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ReturnsLoggedUser()
    {
        var client = await _factory.GetAuthenticatedClient();
        var response = await client.GetAsync("api/Profile/profile");
        response.EnsureSuccessStatusCode();
        var vm = await Utilities.GetResponseContent<ProfileDetailVm>(response);
        Assert.IsType<ProfileDetailVm>(vm);
        Assert.NotNull(vm.Username);
    }
    //Deleted
    //[Fact]
    //public async Task GetNonExistingUser()
    //{
    //    var client = await _factory.GetAuthenticatedClient();
    //    var response = await client.GetAsync("api/Profile/profile?NonExistingUser");
    //    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    //}
    [Fact]
    public async Task AnonymousCantGet()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("api/Profile/profile");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}