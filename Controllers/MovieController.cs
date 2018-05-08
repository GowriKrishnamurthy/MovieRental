using MovieRental.Models;
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
            var movie = new Movie () { Name = "Kung Fu Panda" };
            return View(movie);
        }
    }
}