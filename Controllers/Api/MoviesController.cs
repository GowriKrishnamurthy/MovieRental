﻿using AutoMapper;
using MovieRental.Dtos;
using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace MovieRental.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET all movies - api/Movies
        public IHttpActionResult GetMovies(string query = null)
        {
            // Get movies that are only available
            var moviesQuery = _context.Movies
                .Include(m => m.Genre);

            // Filter based on the query passed, and only pick available movies
            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query) && m.NumberAvailable > 0);

            var movieDto = moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movieDto);
        }
    
        //GET single movie - api/Movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //Create movie - api/Movies/
        [HttpPost]

        // This action is allowed only to the super users (with the role set to CanManageMovies)
        [Authorize(Roles = Constants.CanManageMovies)]

        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            // Validate form entries
            if (!ModelState.IsValid)
                return BadRequest();

            // Here we need to map our DTO to domain object
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            //Movie object has the ID that is generated by the DB.
            //We need to add this ID to our DTO.
            movieDto.Id = movie.Id;

            //Return the Created Status Code as 201 Created(as per the RESTful Convention
            // Param:1-URI,2-object created.
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        //Update movie - api/Movies/1
        [HttpPut]

        // This action is allowed only to the super users (with the role set to CanManageMovies)
        [Authorize(Roles = Constants.CanManageMovies)]

        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            // Validate form entries
            if (!ModelState.IsValid)
                return BadRequest();

            // Check if the movie is available in Db
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            // copy all the values from the Movie DTO object of the request body to the database object
            Mapper.Map(movieDto, movieInDb);

            //Commit the changes from memory to DB
            _context.SaveChanges();
            return Ok();
        }


        //DELETE - api/Movie/1
        [HttpDelete]

        // This action is allowed only to the super users (with the role set to CanManageMovies)
        [Authorize(Roles = Constants.CanManageMovies)]

        public IHttpActionResult DeleteMovie(int id)
        {
            // Check if the movie is available in Db
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            // Remove the particular movie row from the DB context 
            _context.Movies.Remove(movieInDb);

            //Commit the changes from memory to DB
            _context.SaveChanges();
            return Ok();

        }
    }
}


