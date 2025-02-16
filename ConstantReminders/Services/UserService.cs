
using System;
using ConstantReminders.Contracts.Interfaces.Business;
using ConstantReminders.Contracts.Interfaces.Data;
using ConstantReminders.Contracts.Models;

namespace ConstantReminders.Services
{
    public class UserService(IBaseRepository<User> userRepo) : IUserService
    {
       
        public async Task<User> CreateUser(string firstName, string lastName,
               string phoneNumber, string email, string auth0Id, string createdBy)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = email,
                AuthOId = auth0Id,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                CreatedBy = createdBy,
                UpdatedBy = createdBy

            };

           var result =  await userRepo.CreateAsync(newUser);

            return result;
        }

        public async Task <List<User>> GetUsers()
        {

            var result = await userRepo.List();
            return result;
          
            
        }


        public async Task<User?> GetUserById(Guid id)
        {
            return await userRepo.GetByIdAsync(id);
        }


        public async Task<User?> UpdateUser(Guid id, string firstName, string lastName, string phoneNumber,
            string email, string auth0Id, string updatedBy)
        {
            var user = await userRepo.GetByIdAsync(id);
            if (user == null)
                return null;

            user.FirstName = firstName;
            user.LastName = lastName;
            user.PhoneNumber = phoneNumber;
            user.Email = email;
            user.AuthOId = auth0Id;
            user.UpdatedDateTime = DateTime.Now;
            user.UpdatedBy = updatedBy;

            await userRepo.UpdateAsync(user);
            return user;
        }


        public async Task<User?> DeleteUser(Guid id)
        {
            var user = await userRepo.GetByIdAsync(id);
            if (user == null) return null;

            await userRepo.DeleteAsync(user);
            return user;
        }

     
    }
}
