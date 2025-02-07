
using ConstantReminders.Services;



namespace ConstantReminders.Api.Tests.ConstantReminders.Services
{

    public class UserServiceTests
    {
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userService = new UserService();
        }

        [Fact]
        public async Task CreateUserAndReturnProperties()
        {
            string userFirstName = "Sam";
            string userLastName = "Doe";
            string userPhoneNumber = "555-5555";
            string userEmail = "sam@example.com";
            string userAuthOId = "authO422";
            string updatedBy = "adminUpdated";

            var user =  _userService.CreateUser(userFirstName, userLastName, userPhoneNumber, userEmail, userAuthOId, updatedBy);

            Assert.NotNull(user);
            Assert.Equal(userFirstName, user.FirstName);
            Assert.Equal(userLastName, user.LastName);
            Assert.Equal(userPhoneNumber, user.PhoneNumber);
            Assert.Equal(userEmail, user.Email);
            Assert.Equal(userAuthOId, user.AuthOId);
            Assert.Equal(updatedBy, user.UpdatedBy);
            Assert.NotEqual(user.UpdatedDateTime, user.UpdatedDateTime);

        }


        [Fact]
        public async Task UpdateUser_ShouldReturnNull_WhenNotFound()
        {
            var user = _userService.UpdateUser(Guid.NewGuid(), "Sam", "Doe", "555-5555", "sam@example.com", "auth0422", "adminUpdated");

            Assert.Null(user);
        }



        [Fact]
        public async Task DeleteUser_ShouldDeleteUser()
        {
           
            var user = _userService.CreateUser("Sam", "Doe", "555-5555", "sam@example.com", "auth0422", "admin");
           
            var isDeleted = _userService.DeleteUser(user.Id);

           
            Assert.True(isDeleted);
            var foundUser = _userService.GetUserById(user.Id);
            Assert.Null(foundUser);
        }



        [Fact]
        public async Task DeleteUser()
        {
            var isDeleted = _userService.DeleteUser(Guid.NewGuid());

            Assert.False(isDeleted);
        }
    }
}
