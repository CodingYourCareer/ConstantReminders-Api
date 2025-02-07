
using ConstantReminders.Contracts.Models;

namespace ConstantReminders.Services
{
    public class UserService
    {
        private List<User> users;

        public UserService() 
        { 
            users = new List<User>();
        }

        //Create a new user and adds it to the users
        public User CreateUser(string firstName, string lastName,
               string phoneNumber, string email,
               string auth0Id, string createdBy)
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

            users.Add(newUser);
            return newUser;

        }

        // Returns a list of all users 
        public List<User> GetUsers()
        {
            return users;

        }

        // Returns a user based on the given Id. No user found, returns null
        public User? GetUserById(Guid id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }

        //Updates a user's information. If the user is not, return null
        public User? UpdateUser (Guid id, string firstname,
            string lastname, string phoneNumber,string email, 
            string auth0Id, string createdBy)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return null;

            user.FirstName = firstname;
            user.LastName = lastname;
            user.PhoneNumber = phoneNumber;
            user.Email = email;
            user.AuthOId = auth0Id;
            user.UpdatedDateTime = DateTime.Now;
            user.UpdatedBy = createdBy;

            return user;

        }

        //Remove a user by their Id. If no user is found, returns false
        public bool DeleteUser(Guid id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return false;

            users.Remove(user);
            return true;

        }

    }
}
