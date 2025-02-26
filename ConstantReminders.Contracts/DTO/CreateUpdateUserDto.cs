using ConstantReminders.Contracts.Attribute;
using ConstantReminders.Contracts.Models;
using System.ComponentModel.DataAnnotations;

namespace ConstantReminders.Contracts.DTO;

[PhoneOrEmailRequired]
public class CreateUpdateUserDto
{
    [Required]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "First name must be between 1 and 100 characters.")]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Last name must be between 1 and 100 characters.")]
    public string LastName { get; set; }

    [Phone]
    [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
    public string? PhoneNumber { get; set; }

    [EmailAddress]
    [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
    public string? Email { get; set; }

    [Required]
    public DefaultCommunicationMethod DefaultCommunicationMethod { get; set; }
}
