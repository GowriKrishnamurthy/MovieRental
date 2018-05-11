using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRental.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter{ get; set; }

        // Associate Customer Model class with MembershipType
        public MembershipType MembershipType { get; set; }

        //foreign key
        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

        [Display(Name="Date of Birth")]
        // Use custom validation
        [Min18YearsForMembership]
        // Birthdate is nullable
        public DateTime? Birthdate { get; set; }
    }
}