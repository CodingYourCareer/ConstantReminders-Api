using System;
using System.ComponentModel.DataAnnotations;
using ConstantReminders.Data.Model;

namespace  ConstantReminders.Data.Attribute 
{
    public class PhoneOrEmailRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var user = (User)validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(user.PhoneNumber) && string.IsNullOrEmpty(user.Email))
            {
                return new ValidationResult("Either phone number or email is requried.");
            }
            return ValidationResult.Success;
        }
    }

}