using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieRental.Models
{
    [Table("Rental")]
    public class Rental
    {
        public int Id { get; set; }

        // Associate Rental domain model class with Customer and Movie
        [Required]
        public Customer Customer{ get; set; }

        [Required]
        public Movie Movie{ get; set; }

        // Date rented will be marked to current date time.
        // Not an input field from customer.
        [Display(Name = "Date Rented")]
        public DateTime DateRented { get; set; }

        //Date Returned can be nullable
        [Display(Name = "Date Returned")]
        public DateTime? DateReturned { get; set; }
    }
}