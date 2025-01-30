using System;
using System.ComponentModel.DataAnnotations;
using ConstantReminders.Contracts.Models;

namespace  ConstantReminders.Data.Attribute 
{
    public class PhoneOrEmailRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var user = (User)validationContext.ObjectInstance;

            if (string.IsNullOrWhiteSpace(user.PhoneNumber) && string.IsNullOrWhiteSpace(user.Email))
            {
                return new ValidationResult("Either phone number or email is requried.");
            }
            return ValidationResult.Success;
        }
    }

}