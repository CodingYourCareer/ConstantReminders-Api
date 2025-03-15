using ConstantReminders.Contracts.DTO;
using ConstantReminders.Contracts.Models;
using ConstantReminders.Contracts.Mapper;

namespace ConstantReminders.Api.Tests.ConstantReminders.Mapper;

public class UserMapperTests
{
    [Fact]
    public void ToEntity_ShouldMapDtoToUserEntity()
    {
        // Arrange
        var dto = new CreateUpdateUserDto
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "555-555-5555",
            Email = "john.doe@email.com",
            DefaultCommunicationMethod = DefaultCommunicationMethod.Email
        };

        string createdBy = "Admin";
        string auth0Id = "auth0-123";

        // Act
        var user = dto.ToEntity(createdBy, auth0Id);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(dto.FirstName, user.FirstName);
        Assert.Equal(dto.LastName, user.LastName);
        Assert.Equal(dto.PhoneNumber, user.PhoneNumber);
        Assert.Equal(dto.Email, user.Email);
        Assert.Equal(dto.DefaultCommunicationMethod, user.DefaultCommunicationMethod);
        Assert.Equal(auth0Id, user.AuthOId);
        Assert.Equal(createdBy, user.CreatedBy);
    }

    [Fact]
    public void ToDto_ShouldMapUserEntityToUserDto()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Jane",
            LastName = "Doe",
            PhoneNumber = "123-456-7890",
            Email = "jane.doe@email.com",
            DefaultCommunicationMethod = DefaultCommunicationMethod.Phone,
            AuthOId = "auth0-xyz",
            CreatedBy = "testuser"
        };

        // Act
        var dto = user.ToDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(user.Id, dto.Id);
        Assert.Equal(user.FirstName, dto.FirstName);
        Assert.Equal(user.LastName, dto.LastName);
        Assert.Equal(user.PhoneNumber, dto.PhoneNumber);
        Assert.Equal(user.Email, dto.Email);
        Assert.Equal(user.DefaultCommunicationMethod, dto.DefaultCommunicationMethod);
        Assert.Equal(user.AuthOId, dto.AuthOId);
    }
}
