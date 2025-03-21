namespace ConstantReminders.Contracts.Interfaces.Business
{
    public interface IEmailService
    {
        Task SendMailAsync(string to, string subject, string message, string? templateId = null);
    }
}
