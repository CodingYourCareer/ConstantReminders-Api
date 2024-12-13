namespace ConstantReminders-Api.Tests;

using Microsoft.AspNetCore.Mvc.Testing;


public class UnitTest1
{
    [Fact]
   public async Task TestRootEndpoint(){

    await using var application = new WebApplicationFactory<AppProgram>();
    using var client = application.CreateClient();
    var response = await client.GetStringAsync("/health");
    
    Assert.Equal("{\"status\":\"healthy\"}", response);

    }
}
