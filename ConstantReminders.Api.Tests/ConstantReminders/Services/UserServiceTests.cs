using ConstantReminders.Contracts.DTO;
using ConstantReminders.Contracts.Interfaces.Data;
using ConstantReminders.Contracts.Mapper;
using ConstantReminders.Contracts.Models;
using ConstantReminders.Services;
using NSubstitute;


namespace ConstantReminders.Api.Tests.ConstantReminders.Services;

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
        const DefaultCommunicationMethod communicationMethod = DefaultCommunicationMethod.Email;

        var dto = new CreateUpdateUserDto
        {
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Email = email,
            DefaultCommunicationMethod = communicationMethod
        };

        var time = DateTime.UtcNow;

        var createdUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            DefaultCommunicationMethod = communicationMethod,
            CreatedBy = "testUser",
            AuthOId = "testAuth0Id",
            CreatedDateTime = time,
            UpdatedDateTime = time
        };

        mockRepo.CreateAsync(Arg.Any<User>())
            .Returns(Task.FromResult(createdUser));

        var response = await service.CreateUser(dto, "testUser", "testAuth0Id");

        await mockRepo.Received(1).CreateAsync(Arg.Any<User>());

        Assert.Equal(firstName, response.FirstName);
        Assert.Equal(lastName, response.LastName);
        Assert.Equal(email, response.Email);
        Assert.Equal(phoneNumber, response.PhoneNumber);
        Assert.Equal(communicationMethod, response.DefaultCommunicationMethod);
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
        var existingUser = new User
        {
            Id = userId,
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "555-555-5555",
            Email = "john@test.com",
            AuthOId = "1",
            CreatedBy = "user",
            UpdatedBy = "user",
            CreatedDateTime = DateTime.UtcNow,
            UpdatedDateTime = DateTime.UtcNow
        };

        var updateDto = new CreateUpdateUserDto
        {
            FirstName = "John",
            LastName = "Updated",
            PhoneNumber = "123456789",
            Email = "john.updated@email.com",
            DefaultCommunicationMethod = DefaultCommunicationMethod.Email
        };

        // Mock repository behavior: Return existing user when queried
        mockRepo.GetByIdAsync(userId).Returns(existingUser);

        User? capturedUpdatedUser = null;

        // Capture what is passed to UpdateAsync instead of assuming correctness
        mockRepo.UpdateAsync(Arg.Do<User>(u =>
        {
            capturedUpdatedUser = new User
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                Email = u.Email,
                DefaultCommunicationMethod = u.DefaultCommunicationMethod,
                AuthOId = u.AuthOId,
                CreatedBy = u.CreatedBy,
                UpdatedBy = u.UpdatedBy,
                CreatedDateTime = u.CreatedDateTime,
                UpdatedDateTime = u.UpdatedDateTime
            };
        })).Returns(callInfo => Task.FromResult(callInfo.Arg<User>()));

        // Act: Call UpdateUser with DTO
        var response = await service.UpdateUser(updateDto, userId, "User");

        // Assert repository interactions
        await mockRepo.Received(1).GetByIdAsync(userId);
        await mockRepo.Received(1).UpdateAsync(Arg.Any<User>());

        // Assertions on the modified user BEFORE it's saved
        Assert.NotNull(capturedUpdatedUser);
        Assert.Equal(updateDto.LastName, capturedUpdatedUser.LastName);
        Assert.Equal(updateDto.PhoneNumber, capturedUpdatedUser.PhoneNumber);
        Assert.Equal(updateDto.Email, capturedUpdatedUser.Email);
        Assert.Equal(DefaultCommunicationMethod.Email, capturedUpdatedUser.DefaultCommunicationMethod);
        Assert.Equal("User", capturedUpdatedUser.UpdatedBy); // Ensuring UpdatedBy is set

        // Assertions on the response DTO
        Assert.NotNull(response);
        Assert.Equal(updateDto.LastName, response.LastName);
        Assert.Equal(updateDto.PhoneNumber, response.PhoneNumber);
        Assert.Equal(updateDto.Email, response.Email);
        Assert.Equal(DefaultCommunicationMethod.Email, response.DefaultCommunicationMethod);
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
        Assert.True(response);
    }
}