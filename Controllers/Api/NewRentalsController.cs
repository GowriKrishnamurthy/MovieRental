using AutoMapper;
using MovieRental.Dtos;
using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MovieRental.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRentalDto)
        {
            // Get customer from rental DTO for the specific ID
            // Using Single() here assuming the client will be sending the right customer id. 

            var customer = _context.Customers.Single(
                c => c.Id == newRentalDto.CustomerId);

            // Get all movies from rental DTO 
            // equivalent SQL statement - select * from Movies where Id in(x,y,z);
            var moviesList = _context.Movies
                .Where(m => newRentalDto.MovieIds.Contains(m.Id))
                .ToList();

            foreach ( var movie in moviesList)
            { 
                // If there is no stock available, no need to create rental object
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available");
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                // Once movie is rented out, decrement the availability.
                movie.NumberAvailable--;

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}