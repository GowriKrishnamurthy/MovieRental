using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieRental.Models
{
    [Table("Movies")]

    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the Name of the movie")]
        [StringLength(255)]
        public string Name { get; set; }

        // Associate with Genre table
        public Genre Genre { get; set; }

        //foreign key
        [Required(ErrorMessage = "Please select the genre of the movie")]
        public byte GenreId { get; set; }

        public DateTime DateAdded { get; set; }

        [Required(ErrorMessage = "Please enter the Release Date.")]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Please enter Number in Stock")]
        [Display(Name = "Number in Stock")]
        [Range(1,20)]
        public byte NumberInStock { get; set; }
    }
}