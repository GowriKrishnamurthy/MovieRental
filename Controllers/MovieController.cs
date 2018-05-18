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
            // var movies = _context.Movies.Include(g => g.Genre).ToList();
            // return View(movies);

            // Load views depending on the User role
            if (User.IsInRole(Constants.CanManageMovies))
                return View("List");

            return View("ReadOnlyList");
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

        // Show this New movie page only to the users with the role set to CanManageMovies
        [Authorize(Roles = Constants.CanManageMovies)]

        // On click of New Movie button, this method gets called to populate page with defaults
        public ActionResult New()
        {
            var genre= _context.Genres.ToList();

            // Create a Viewmodel as we need a view that combines 2 entities - Movie and Genre
            var viewModel = new MovieFormViewModel
            {
                // Commented this line below to remove initial values on the Movie Form
                // Movie= new Movie(),
                Genre= genre
            };
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //This method gets called when Save button is clicked on new Movie form
        public ActionResult Save(Movie movie)
        {
            // Form level Validation check
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genre = _context.Genres.ToList()
                };
                //If form validation failed - return to same form.
                return View("MovieForm", viewModel);
            }

            //New movie
            if (movie.Id == 0)
            {
                // Added time is the current system time.
                movie.DateAdded = DateTime.Now;
                movie.NumberAvailable = movie.NumberInStock;
                _context.Movies.Add(movie);
            }
            //existing movie - Edit changes
            else
            {
                //Single method used - throws exception if movie was not found with this ID
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId= movie.GenreId;

                //Added time will not be allowed to be edited
                //movieInDb.DateAdded = movie.DateAdded;
                movieInDb.ReleaseDate= movie.ReleaseDate;
                movieInDb.NumberAvailable = movie.NumberInStock;
                //Alternative options to save to Db
            }
            _context.SaveChanges();

            //Once the changes from DB contexts are committed to DB, Home page of movie gets loaded
            return RedirectToAction("Index", "Movie");
        }

        // Only authorized users can edit 
        [Authorize(Roles = Constants.CanManageMovies)]

        // On click of Edit movie button, this method gets called
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            // Create a Viewmodel as we need a view that combines 2 entities - Movie and Genre
            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genre= _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }
    }
}