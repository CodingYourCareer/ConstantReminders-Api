using ConstantReminders.Contracts.DTO;

namespace ConstantReminders.Contracts.Interfaces.Business;
public interface IUserService
{
    Task<UserDto> CreateUser(CreateUpdateUserDto dto, string createdBy, string auth0Id);
    Task<List<UserDto>> GetUsers();
    Task<UserDto?> GetUserById(Guid id);
    Task<UserDto?> UpdateUser(CreateUpdateUserDto dto, Guid id, string updatedBy);
    Task<bool> DeleteUser(Guid id);
}
