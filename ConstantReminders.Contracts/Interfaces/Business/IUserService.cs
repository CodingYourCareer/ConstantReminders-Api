using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConstantReminders.Contracts.Models;

namespace ConstantReminders.Contracts.Interfaces.Business
{
    public interface IUserService
    {

        Task<User> CreateUser(string firstName, string lastName,
              string phoneNumber, string email, string auth0Id, string createdBy);

        Task<List<User>> GetUsers();

        Task<User?> GetUserById(Guid id);


        Task<User?> UpdateUser(Guid id, string firstName, string lastName, string phoneNumber,
            string email, string auth0Id, string updatedBy);

        Task<User?> DeleteUser(Guid id);
    



    }
}
