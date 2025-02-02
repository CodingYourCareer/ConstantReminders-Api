using System;
using System.ComponentModel.DataAnnotations;
using ConstantReminders.Contracts.Models;

namespace  ConstantReminders.Contracts.Attribute;

    public class PhoneOrEmailRequiredAttribute : ValidationAttribute
    {
         protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult("Validation failed: Value cannot be null.");
            }

            if (validationContext.ObjectInstance is not User user)
            {
                return new ValidationResult("Validation failed: ObjectInstance is invalid.");
            }

            if (string.IsNullOrWhiteSpace(user.PhoneNumber) && string.IsNullOrWhiteSpace(user.Email))
            {
                return new ValidationResult("Either phone number or email is required.");
            }

            return ValidationResult.Success;
        }
    }

