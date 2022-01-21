using System.Net;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Models;
using JustAnotherToDo.Application.Profiles.Queries.GetProfilesWithPagination;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using WebUI.Controllers;
using Xunit;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Profile;

public class GetPaginated : IClassFixture<CustomWebApplicationFactory<ProfileController>>
{
    private CustomWebApplicationFactory<ProfileController> _factory;
    public GetPaginated(CustomWebApplicationFactory<ProfileController> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ReturnsPaginatedProfiles()
    {
        var client = await _factory.GetAuthenticatedClient();
        var response = await client.GetAsync("api/Profile");
        response.EnsureSuccessStatusCode();
        var vm = await Utilities.GetResponseContent<PaginatedList<ProfilesDto>>(response);
        Assert.IsType<PaginatedList<ProfilesDto>>(vm);
        Assert.NotEmpty(vm.Items);
    }
    [Fact]
    public async Task RequestedPageNumberOverActualPageList()
    {
        var client = await _factory.GetAuthenticatedClient();
        var response = await client.GetAsync("api/Profile?pageNumber=100");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Fact]
    public async Task RequestedNegativePageNumber()
    {
        var client = await _factory.GetAuthenticatedClient();
        var response = await client.GetAsync("api/Profile?pageNumber=-1");
        response.EnsureSuccessStatusCode();
        var vm = await Utilities.GetResponseContent<PaginatedList<ProfilesDto>>(response);
        Assert.IsType<PaginatedList<ProfilesDto>>(vm);
        Assert.NotEmpty(vm.Items);
    }
    [Fact]
    public async Task RequestedNegativePageSize()
    {
        var client = await _factory.GetAuthenticatedClient();
        var response = await client.GetAsync("api/Profile?pageSize=-1");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Fact]
    public async Task AnonymousCantGet()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("api/Profile?pageNumber=1");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}