using MovieRental.Models;
using MovieRental.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRental.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult AllMovies()
        {
            var movie = new Movie() { Name = "Kung Fu Panda" };
            var customers = new List<Customer>
            {new Customer{Name="Customer 1"},
            new Customer{Name="Customer 2"}};

            var movieCustCombo = new AllMoviesViewModel
            {
                Movie = movie,
                Customers = customers
            };
            return View(movieCustCombo);
        }
    }
}