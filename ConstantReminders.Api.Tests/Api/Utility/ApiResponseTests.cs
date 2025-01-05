using ConstantReminder.Api.Utility;
using ConstantReminders.Contracts.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;

namespace ConstantReminders.Api.Tests.Api.Utility;

public class ApiResponseTests
{
    [Theory]
    [InlineData(ResultStatus.Ok200, typeof(Ok<ResponseDetail<string?>>), 200)]
    [InlineData(ResultStatus.Created201, typeof(Created<ResponseDetail<string?>>), 201)]
    [InlineData(ResultStatus.Accepted202, typeof(Accepted<ResponseDetail<string?>>), 202)]
    [InlineData(ResultStatus.NoContent204, typeof(NoContent), 204)]
    [InlineData(ResultStatus.PartialContent206, null, 206)]
    [InlineData(ResultStatus.MovedPermanently301, typeof(RedirectHttpResult), 301)]
    [InlineData(ResultStatus.Found302, typeof(RedirectHttpResult), 302)]
    [InlineData(ResultStatus.SeeOther303, typeof(RedirectHttpResult), 303)]
    [InlineData(ResultStatus.NotModified304, typeof(StatusCodeHttpResult), 304)]
    [InlineData(ResultStatus.BadRequest400, typeof(BadRequest<ResponseDetail<string?>>), 400)]
    [InlineData(ResultStatus.NotAuthorized401, typeof(UnauthorizedHttpResult), 401)]
    [InlineData(ResultStatus.Forbidden403, typeof(StatusCodeHttpResult), 403)]
    [InlineData(ResultStatus.NotFound404, typeof(NotFound<ResponseDetail<string?>>), 404)]
    [InlineData(ResultStatus.MethodNotAllowed405, typeof(StatusCodeHttpResult), 405)]
    [InlineData(ResultStatus.Conflict409, typeof(Conflict<ResponseDetail<string?>>), 409)]
    [InlineData(ResultStatus.FailedDependency424, typeof(ProblemHttpResult), 424)]
    [InlineData(ResultStatus.TooManyRequests429, typeof(ProblemHttpResult), 429)]
    [InlineData(ResultStatus.Fatal500, typeof(ProblemHttpResult), 500)]
    [InlineData(ResultStatus.NotImplemented501, typeof(ProblemHttpResult), 501)]
    [InlineData(ResultStatus.BadGateway502, typeof(ProblemHttpResult), 502)]
    [InlineData(ResultStatus.ServiceUnavailable503, typeof(ProblemHttpResult), 503)]
    [InlineData(ResultStatus.GatewayTimeout504, typeof(ProblemHttpResult), 504)]
    public void GetActionResult_ReturnsCorrectIResult_andStatusCode(
        ResultStatus status,
        Type? expectedType,
        int expectedStatusCode)
    {
        // Arrange
        var response = new ResponseDetail<string?>
        {
            Status = status,
            Data = "Test data",
            SubCode = "Sub Code",
            Message = "Message"
        };

        // Act
        var result = ApiResponse.GetActionResult(response);

        // Assert
        if (status == ResultStatus.PartialContent206)
        {
            var jsonResult = Assert.IsType<JsonHttpResult<ResponseDetail<string?>>>(result);
            Assert.Equal(206, jsonResult.StatusCode);
        }
        else
        {
            Assert.NotNull(expectedType);
            Assert.IsType(result.GetType(), result);
        }
    }

    [Fact]
    public void GetActionResult_UsesCustomResourceName_AndBuildsCreatedUri()
    {
        // Arrange
        var entity = Substitute.For<Event>();
        entity.Id.Returns(Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeffffffff"));

        var response = new ResponseDetail<Event>
        {
            Status = ResultStatus.Created201,
            Data = entity,
            SubCode = "Sub Code",
            Message = "Message"
        };
        const string resourceName = "customresource";

        // Act
        var result = ApiResponse.GetActionResult(response, resourceName);

        // Assert
        var createdResult = Assert.IsType<Created<ResponseDetail<Event>>>(result);
        Assert.Equal(201, createdResult.StatusCode);

        const string expectedUri = "/customresource/aaaaaaaa-bbbb-cccc-dddd-eeeeffffffff";
        Assert.Equal(expectedUri, createdResult.Location);
    }

    [Fact]
    public void GetActionResult_DefaultResourcePath_IfResourceNameNotProvided()
    {
        // Arrange
        var testEntity = Substitute.For<Event>();
        testEntity.Id.Returns(Guid.NewGuid());

        var response = new ResponseDetail<Event>
        {
            Status = ResultStatus.Ok200,
            Data = testEntity,
            SubCode = "Sub Code",
            Message = "Message"
        };

        // Act
        var result = ApiResponse.GetActionResult(response);

        var okResult = Assert.IsType<Ok<ResponseDetail<Event>>>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public void GetListActionResult_ReturnsOk_IfStatusIsOk200_AndHasListData()
    {
        // Arrange
        var response = new ResponseDetail<List<int>>
        {
            Status = ResultStatus.Ok200,
            Data = [1, 2, 3],
            SubCode = "Sub Code",
            Message = "Message"
        };

        // Act
        var result = ApiResponse.GetListActionResult(response);

        // Assert
        var okResult = Assert.IsType<Ok<ResponseDetail<List<int>>>>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public void GetListActionResult_FirstItemDeterminesResourceId_IfImplementsEvent()
    {
        // Arrange
        var entity1 = Substitute.For<Event>();
        entity1.Id.Returns(Guid.Parse("11111111-2222-3333-4444-555555555555"));

        var entity2 = Substitute.For<Event>();
        entity2.Id.Returns(Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeffffffff"));

        var dataList = new List<Event> { entity1, entity2 };
        var response = new ResponseDetail<List<Event>>
        {
            Status = ResultStatus.Created201,
            Data = dataList,
            SubCode = "Sub Code",
            Message = "Message"
        };

        // Act
        var result = ApiResponse.GetListActionResult(response, "myentities");

        // Assert
        var createdResult = Assert.IsType<Created<ResponseDetail<List<Event>>>>(result);
        Assert.Equal(201, createdResult.StatusCode);

        const string expectedLocation = "/myentities/11111111-2222-3333-4444-555555555555";
        Assert.Equal(expectedLocation, createdResult.Location);
    }

    [Fact]
    public void GetListActionResult_EmptyList_NoResourceId()
    {
        // Arrange
        var response = new ResponseDetail<List<Event>>
        {
            Status = ResultStatus.Ok200,
            Data = [],
            SubCode = "Sub Code",
            Message = "Message"
        };

        // Act
        var result = ApiResponse.GetListActionResult(response, "myentities");

        // Assert
        var okResult = Assert.IsType<Ok<ResponseDetail<List<Event>>>>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
}