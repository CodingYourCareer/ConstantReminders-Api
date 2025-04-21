using ConstantReminders.Contracts.Interfaces.Business;
using ConstantReminders.Contracts.Interfaces.Data;
using ConstantReminders.Contracts.Models;
using ConstantReminders.Services;
using NSubstitute;

namespace ConstantReminders.Api.Tests.ConstantReminders.Services;
    public class TwilioServiceTests
{
    private readonly ITwilioService _mockTwilioService = Substitute.For<ITwilioService>();

    [Fact]
    public async Task TwilioPhoneMessage_ReturnsCorrect_Response()
    {
        const string userId = "TestUserId";
        const string userToken = "TestUserToken";
        const TwilioPhoneMessage userMessage = const new TwilioPhoneMessage
        {

            Id = Guid.NewGuid(),
            CreatedDateTime = DateTime.Today,
            UpdatedDateTime = DateTime.Today,
            CreatedBy = "test user",
            UpdatedBy = "test admin",
            PhoneNumber = "test number",
            PhoneMessage = "test message",

        }
        var expectedResponse = new TwilioResponse { isSuccessful = true, errorMessage = "none" };

}
    
    

