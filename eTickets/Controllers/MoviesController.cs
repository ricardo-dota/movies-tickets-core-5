using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _servicio;
        public MoviesController(IMoviesService servicio)
        {
            _servicio = servicio;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.allMovies = await _servicio.Getall( n => n.Cinema);
            
            return View();
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _servicio.Getall(n => n.Cinema);

            if( !string.IsNullOrEmpty(searchString))
            {
                var filterResult = allMovies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                ViewBag.allMovies = filterResult;
                return View("Index");
            }


            ViewBag.allMovies = allMovies;
            return View("Index");
        }
        

        //GET : Movies/Details
        public async Task<IActionResult> Details(int id)
        {

            var movieDetail = await _servicio.GetMovieByIdAsync(id);

            return View(movieDetail);
        }

        //GET
        public async Task<IActionResult> Create()
        {
            var movieDropdownsData  = await _servicio.NewMoviesDropdownsValues();
            ViewBag.Cinemas         =  new SelectList( movieDropdownsData.Cinemas , "Id" , "Name") ;
            ViewBag.Producers       = new SelectList( movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors          = new SelectList( movieDropdownsData.Actors, "Id", "FullName");
            return View();
        }

        //POIST

        [HttpPost]

        public async Task<IActionResult> Create(NewMovieVM movie)
        {

            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _servicio.NewMoviesDropdownsValues();
                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
                return View(movie);
            }

            await _servicio.AddNewMovie(movie);
            return RedirectToAction( nameof(Index) );
        }


        //GET
        public async Task<IActionResult> Edit( int id) { 

            var movie = await _servicio.GetMovieByIdAsync(id);

            if (movie == null) return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = movie.Id,
                Name = movie.Name,

                Description = movie.Description,

                Price = movie.Price,

                Image = movie.Image,

                StartDate = movie.StartDate,

                EndDate = movie.EndDate,

                MovieCategory = movie.MovieCategory,

                ProducerId = movie.ProducerId,

                CinemaId = movie.CinemaId,

                ActorIds = movie.Actors_Movies.Select( n => n.ActorId ).ToList(),

            };


            var movieDropdownsData = await _servicio.NewMoviesDropdownsValues();
            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
            return View(response);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, NewMovieVM movie )
        {
            //if (movie == null) return View("NotFound");

            //if (id != movie.Id ) return View("NotFound");

            movie.Id = id;

            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _servicio.NewMoviesDropdownsValues();
                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
                return View(movie);
            }

            await _servicio.UpdateMovie(movie);
            return RedirectToAction(nameof(Index));
        }

    }
}
