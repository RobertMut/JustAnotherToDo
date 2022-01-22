using System.Net;
using System.Threading.Tasks;
using JustAnotherToDo.Application.Models;
using JustAnotherToDo.Application.Profiles.Queries.GetProfilesWithPagination;
using JustAnotherToDo.WebUI.IntegrationTests.Common;
using NUnit.Framework;
using WebUI.Controllers;

namespace JustAnotherToDo.WebUI.IntegrationTests.Controller.Profile;

public class GetPaginated
{
    private CustomWebApplicationFactory<ProfileController> _factory;
    [SetUp]
    public void SetUp()
    {
        _factory = new CustomWebApplicationFactory<ProfileController>();
    }

    [Test]
    public async Task ReturnsPaginatedProfiles()
    {
        var client = await _factory.GetAuthenticatedClient();
        var response = await client.GetAsync("api/Profile");
        response.EnsureSuccessStatusCode();
        var vm = await Utilities.GetResponseContent<PaginatedList<ProfilesDto>>(response);
        Assert.IsInstanceOf<PaginatedList<ProfilesDto>>(vm);
        Assert.NotNull(vm.Items);
    }
    [Test]
    public async Task RequestedPageNumberOverActualPageList()
    {
        var client = await _factory.GetAuthenticatedClient();
        var response = await client.GetAsync("api/Profile?pageNumber=100");
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Test]
    public async Task RequestedNegativePageNumber()
    {
        var client = await _factory.GetAuthenticatedClient();
        var response = await client.GetAsync("api/Profile?pageNumber=-1");
        response.EnsureSuccessStatusCode();
        var vm = await Utilities.GetResponseContent<PaginatedList<ProfilesDto>>(response);
        Assert.IsInstanceOf<PaginatedList<ProfilesDto>>(vm);
        Assert.NotNull(vm.Items);
    }
    [Test]
    public async Task RequestedNegativePageSize()
    {
        var client = await _factory.GetAuthenticatedClient();
        var response = await client.GetAsync("api/Profile?pageSize=-1");
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Test]
    public async Task AnonymousCantGet()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("api/Profile?pageNumber=1");
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}