using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class MoviesService: EntityBaseRepository<Movie> , IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context) {

            _context = context;
        }

        public async Task AddNewMovie(NewMovieVM data)
        {
            var movie = new Movie()
            {
                Name = data.Name,

                Description = data.Description,

                Price = data.Price,

                Image = data.Image,

                StartDate = data.StartDate,

                EndDate = data.EndDate,

                MovieCategory = data.MovieCategory,

                ProducerId = data.ProducerId,

                CinemaId = data.CinemaId
            };

            await _context.Movies.AddAsync(movie);

            await _context.SaveChangesAsync();

            //add molvie actors

            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie() {

                    MovieId = movie.Id,
                    ActorId = actorId,
                };

                await _context.Actors_Movies.AddAsync(newActorMovie);
                await _context.SaveChangesAsync();

            }
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {

            var movieDetails = await _context.Movies
                    .Include(c => c.Cinema)
                    .Include(p => p.Producer)
                    .Include(am => am.Actors_Movies)
                    .ThenInclude(a => a.Actor)
                    .FirstOrDefaultAsync();

            return  movieDetails;
        }

        public async Task<NewMoviesDropdownsVM> NewMoviesDropdownsValues()
        {
            var response = new NewMoviesDropdownsVM() {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
             };


            return response;
        }

        public async Task UpdateMovie(NewMovieVM data)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync( n => n.Id == data.Id);

            if ( dbMovie != null )
            {

                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.Image = data.Image;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;
                dbMovie.CinemaId = data.CinemaId;

                await _context.SaveChangesAsync();
            }

            //remove exists actor

            var existingActorDB = _context.Actors_Movies.Where( n => n.MovieId == data.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorDB);
            await _context.SaveChangesAsync();

            //add molvie actors

            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {

                    MovieId = data.Id,
                    ActorId = actorId,
                };

                await _context.Actors_Movies.AddAsync(newActorMovie);
               
            }
            await _context.SaveChangesAsync();
        }
    }
}
