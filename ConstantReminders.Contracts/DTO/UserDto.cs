using ConstantReminders.Contracts.Models;
using System.ComponentModel.DataAnnotations;

namespace ConstantReminders.Contracts.DTO;
public class UserDto
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public required string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public DefaultCommunicationMethod DefaultCommunicationMethod { get; set; }
    public required string AuthOId { get; set; }
}
