
using ConstantReminders.Contracts.Interfaces.Data;
using ConstantReminders.Contracts.Models;
using ConstantReminders.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NSubstitute;



namespace ConstantReminders.Api.Tests.ConstantReminders.Services
{

    public class UserServiceTests
    {

        [Fact]
        public async Task CreateUser_ShouldCreateAndReturnUserWithCreated201()
        {
            var mockRepo = Substitute.For<IBaseRepository<User>>();
            var service = new UserService(mockRepo);

            const string firstName = "John";
            const string lastName = "Doe";
            const string email = "test@email.com";
            const string phoneNumber = "555-555-5555";
            const string auth0Id = "1";
            const string createdBy = "User";

            var createdUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                AuthOId = auth0Id,
                CreatedBy = createdBy,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                UpdatedBy = createdBy
            };


            mockRepo.CreateAsync(Arg.Any<User>())
                .Returns(createdUser);

            var response = await service.CreateUser(firstName, lastName, phoneNumber, email, auth0Id, createdBy);

            await mockRepo.Received(1).CreateAsync(Arg.Any<User>());


            Assert.Equal(firstName, response.FirstName);
            Assert.Equal(lastName, response.LastName);
            Assert.Equal(email, response.Email);
            Assert.Equal(phoneNumber, response.PhoneNumber);
            Assert.Equal(auth0Id, response.AuthOId);
            Assert.Equal(createdBy, response.CreatedBy);
            Assert.True(response.CreatedDateTime != default(DateTime));
            Assert.True(response.UpdatedDateTime != default(DateTime));


        }


        [Fact]

        public async Task GetUsers_ShouldReturnListOfUsers()
        {
            var mockRepo = Substitute.For<IBaseRepository<User>>();
            var service = new UserService(mockRepo);

            var users = new List<User>()
            {
                new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", PhoneNumber = "555-555-5555",
                    Email = "john@test.com", AuthOId = "1", CreatedBy = "user", UpdatedBy = "user" },
                new User { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith", PhoneNumber = "444-444-4444",
                    Email ="jane@test.com", AuthOId ="2", CreatedBy = "user", UpdatedBy = "user" }

            };

            mockRepo.List().Returns(users);

            var response = await service.GetUsers();

            await mockRepo.Received(1).List();

            Assert.NotNull(response);
            Assert.Equal(2, response.Count);
            Assert.Equal("John", response[0].FirstName);
            Assert.Equal("Jane", response[1].FirstName);

        }

        [Fact]
        public async Task GetUserById_ShouldReturnUser()
        {
     
            var mockRepo = Substitute.For<IBaseRepository<User>>();
            var service = new UserService(mockRepo);

            var userId = Guid.NewGuid();

            var user = new User 
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "555-555-5555",
                Email = "john@test.com",
                AuthOId = "1",
                CreatedBy = "user",
                UpdatedBy = "user"
            };

       
            mockRepo.GetByIdAsync(userId).Returns(user);

            var response = await service.GetUserById(userId);

            await mockRepo.Received(1).GetByIdAsync(userId);

            Assert.NotNull(response);
            Assert.Equal(userId, response.Id);
            Assert.Equal("John", response.FirstName);
        }

        [Fact]
        public async Task UpdateUser_ShouldUpdateAndReturnUser()
        {
    
            var mockRepo = Substitute.For<IBaseRepository<User>>();
            var service = new UserService(mockRepo);

   
            var userId = Guid.NewGuid();
            var existingUser = new User { 
                Id = userId, 
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "555-555-5555",
                Email = "john@test.com",
                AuthOId = "1",
                CreatedBy = "user",
                UpdatedBy = "user"
            };

           
            var updatedUser = new User
            {
                Id = userId,
                FirstName = "John",
                LastName = "Updated",
                PhoneNumber = "123456789",
                Email = "john.updated@email.com",
                AuthOId = "auth0-id",
                UpdatedBy = "User",
                CreatedBy = "User",
                UpdatedDateTime = DateTime.Now
            };

       
            mockRepo.GetByIdAsync(userId).Returns(existingUser);

          
            mockRepo.UpdateAsync(Arg.Any<User>()).Returns(Task.CompletedTask);  

   
            var response = await service.UpdateUser(userId, updatedUser.FirstName, updatedUser.LastName,
                updatedUser.PhoneNumber, updatedUser.Email, updatedUser.AuthOId, updatedUser.UpdatedBy);

        
            await mockRepo.Received(1).GetByIdAsync(userId);

         
            await mockRepo.Received(1).UpdateAsync(Arg.Any<User>());

        
            Assert.NotNull(response);
            Assert.Equal("Updated", response.LastName);
            Assert.Equal("123456789", response.PhoneNumber);
            Assert.Equal("john.updated@email.com", response.Email);
        }


        [Fact]
        public async Task DeleteUser_ShouldDeleteAndReturnUser()
        {
           
            var mockRepo = Substitute.For<IBaseRepository<User>>();
            var service = new UserService(mockRepo);

            var userId = Guid.NewGuid();

            var user = new User 
            {   Id = userId, 
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "555-555-5555",
                Email = "john@test.com",
                AuthOId = "1",
                CreatedBy = "user",
                UpdatedBy = "user"
            };

          
            mockRepo.GetByIdAsync(userId).Returns(user);

            mockRepo.DeleteAsync(Arg.Any<User>()).Returns(Task.CompletedTask);

           
            var response = await service.DeleteUser(userId);

            await mockRepo.Received(1).GetByIdAsync(userId);

            await mockRepo.Received(1).DeleteAsync(Arg.Any<User>());

          
            Assert.NotNull(response);
            Assert.Equal(userId, response.Id);
        }
    }
}