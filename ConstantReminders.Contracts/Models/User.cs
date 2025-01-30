using System.ComponentModel.DataAnnotations;
using ConstantReminders.Data.Attribute;


namespace ConstantReminders.Contracts.Models
{
    [PhoneOrEmailRequired]
    public class User 
    {
        [Key]
        public int Id { get; set;}
        public string? FirstName {get; set;}
        public required string LastName {get; set;}
        public string? PhoneNumber {get; set;}
        [EmailAddress]
        public string? Email {get; set;}
        public DefaultCommunicationMethod DefaultCommunicationMethod {get; set;}
        public required string AuthOId {get; set;}
            }
}