using ConstantReminders.Contracts.DTO;
using ConstantReminders.Contracts.Models;

namespace ConstantReminders.Contracts.Mapper;

public static class UserMapper
{
    public static User ToEntity(this CreateUpdateUserDto dto, string createdBy, string auth0Id)
    {
        return new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            DefaultCommunicationMethod = dto.DefaultCommunicationMethod,
            AuthOId = auth0Id,
            CreatedBy = createdBy
        };
    }

    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            DefaultCommunicationMethod = user.DefaultCommunicationMethod,
            AuthOId = user.AuthOId
        };
    }
}
