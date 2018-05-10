using MovieRental.Models;
using MovieRental.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace MovieRental.Controllers
{
    public class MovieController : Controller
    {
        //Adding a new view result to be called from Navbar link
        private ApplicationDbContext _context;
        public MovieController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // Index method to show all movies from table
        public ViewResult Index()
        {
            // Include data from 2 tables - Movie and Genre
            var movies = _context.Movies.Include(g => g.Genre).ToList();

            return View(movies);
        }

        // Details method to show details of each movie id
        public ActionResult Details(int id)
        {
            /* 
             * SingleOrDefault - Returns the only element of a sequence, or a default value if the sequence is empty; 
             * this method throws an exception if there is more than one element in the sequence.
            */
            var movies = _context.Movies.Include(m => m.Genre).SingleOrDefault(c => c.Id == id);

            if (movies == null)
                return HttpNotFound();

            return View(movies);
        }
        
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