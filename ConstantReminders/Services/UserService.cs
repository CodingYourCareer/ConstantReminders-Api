using ConstantReminders.Contracts.DTO;
using ConstantReminders.Contracts.Interfaces.Business;
using ConstantReminders.Contracts.Interfaces.Data;
using ConstantReminders.Contracts.Mapper;
using ConstantReminders.Contracts.Models;

namespace ConstantReminders.Services
{
    public class UserService(IBaseRepository<User> userRepo) : IUserService
    {
       
        public async Task<UserDto> CreateUser(CreateUpdateUserDto dto, string createdBy, string auth0Id)
        {
            var newUser = dto.ToEntity(createdBy, auth0Id);

            newUser.CreatedDateTime = DateTime.UtcNow;

            var result =  await userRepo.CreateAsync(newUser);

            return result.ToDto();
        }

        public async Task <List<UserDto>> GetUsers()
        {
            var result = await userRepo.List();
            return result.Select(x => x.ToDto()).ToList();
        }


        public async Task<UserDto?> GetUserById(Guid id)
        {
            var result = await userRepo.GetByIdAsync(id);
            return result?.ToDto();
        }


        public async Task<UserDto?> UpdateUser(CreateUpdateUserDto dto, Guid id, string updatedBy)
        {
            var user = await userRepo.GetByIdAsync(id);
            if (user == null)
                return null;

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.PhoneNumber = dto.PhoneNumber;
            user.Email = dto.Email;
            user.DefaultCommunicationMethod = dto.DefaultCommunicationMethod;
            user.UpdatedDateTime = DateTime.UtcNow;
            user.UpdatedBy = updatedBy;

            await userRepo.UpdateAsync(user);
            return user.ToDto();
        }


        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await userRepo.GetByIdAsync(id);
            if (user == null) return false;

            await userRepo.DeleteAsync(user);

            return true;
        }
    }
}
