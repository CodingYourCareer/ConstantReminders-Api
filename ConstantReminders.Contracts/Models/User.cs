using System.ComponentModel.DataAnnotations;
using ConstantReminders.Contracts.Attribute;


namespace ConstantReminders.Contracts.Models;

    [PhoneOrEmailRequired]
    public class User : IEntity 
    {
        public Guid Id { get; set;}
        public string? FirstName {get; set;}
        public required string LastName {get; set;}
        public string? PhoneNumber {get; set;}
        [EmailAddress]
        public string? Email {get; set;}
        public DefaultCommunicationMethod DefaultCommunicationMethod {get; set;}
        public required string AuthOId {get; set;}
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public required string CreatedBy { get; set; }
        public required string UpdatedBy { get; set; }
            }
