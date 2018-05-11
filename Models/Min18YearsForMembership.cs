using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRental.Models
{
    // Custom Validation logic for membership type.
    // Customer should be abobe 18 years of age to be a member.
    // Mmbership Type - "Pay as you go' is allowed for customers of all ages.
    public class Min18YearsForMembership : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            // If no membership was entered or if 'Pay as you go' was selected, 
            // allow the page to go through.
            // Membership field has a separate logic to show if not entered.
            if (customer.MembershipTypeId == (byte)MembershipType.MembershipTypeName.Unknown 
                || customer.MembershipTypeId == (byte)MembershipType.MembershipTypeName.PayAsYouGo)
            {
                //Pay as you go - allowed for all customers regardless of their age
                return ValidationResult.Success;
            }

            //If no birthdate entered
            if (customer.Birthdate == null)
            {
                return new ValidationResult("Birthdate is required");
            }

            // Calculating the approximate age  
            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            //Neat way of coding ternary oprator
            return (age > 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer should be atleast 18 years old to go on a membership");
        }
    }
}