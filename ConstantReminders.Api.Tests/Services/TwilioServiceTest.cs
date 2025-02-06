using System;
using ConstantReminders.Contracts.Interfaces.Business;
using Xunit;

namespace ConstantReminders.Api.Tests.Services;

public class TwilioServiceTest
{

private readonly ITwilioService _twilioService;

public TwilioServiceTest(ITwilioService twilioService){
    _twilioService = twilioService;
}


[Fact]
public async Task SendSms_Succeeds(){ 

await _twilioService.SendSms("+2512748694","I love Java");


var x = 5;
}


}
