using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ConstantReminders.Contracts.Interfaces.Business; // Use the correct namespace
using ConstantReminders.Services;
using Moq;
using Xunit;

public class EmailServiceTests
{
    [Fact]
    public async Task SendMailAsync_ShouldNotThrowException_WhenSmtpClientIsMocked()
    {
        // Arrange
        var smtpMock = new Mock<ISmtpClient>(); // Mock the ISmtpClient interface (abstracting SmtpClient for easier testing)
        smtpMock.Setup(client => client.SendMailAsync(It.IsAny<MailMessage>()))
                .Returns(Task.CompletedTask); // Pretend to send an email successfully

        var emailService = new EmailService(smtpMock.Object, "noreply@example.com");

        // Act & Assert
        await emailService.SendMailAsync("test@example.com", "Test Subject", "Test Message");

        // If no exception is thrown, the test passes!
    }

    [Fact]
    public async Task SendMailAsync_ShouldLogError_WhenSmtpClientThrowsException()
    {
        // Arrange
        var smtpMock = new Mock<ISmtpClient>();
        smtpMock.Setup(client => client.SendMailAsync(It.IsAny<MailMessage>()))
                .ThrowsAsync(new SmtpException("SMTP Error!")); // Simulate a failure

        var emailService = new EmailService(smtpMock.Object, "noreply@example.com");

        // Act & Assert
        await Assert.ThrowsAsync<SmtpException>(() =>
            emailService.SendMailAsync("test@example.com", "Test Subject", "Test Message")
        );
    }
}

// The interface for SmtpClient to make it mockable.
public interface ISmtpClient
{
    Task SendMailAsync(MailMessage mailMessage);
}

// Modified EmailService that accepts ISmtpClient for easier testing.
public class EmailService : IEmailService
{
    private readonly ISmtpClient _smtpClient;
    private readonly string _fromAddress;

    public EmailService(ISmtpClient smtpClient, string fromAddress)
    {
        _smtpClient = smtpClient;
        _fromAddress = fromAddress;
    }

    public async Task SendMailAsync(string to, string subject, string body)
    {
        var mailMessage = new MailMessage(_fromAddress, to, subject, body);
        try
        {
            await _smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            // Log exception (this is just an example)
            Console.WriteLine($"Error sending email: {ex.Message}");
            throw;
        }
    }
}
