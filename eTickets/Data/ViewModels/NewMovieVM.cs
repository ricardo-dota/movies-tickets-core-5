using eTickets.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.ViewModels
{
    public class NewMovieVM
    {
        public int Id;

        [Required(ErrorMessage = "Nombre es obligatorio")]
        [Display(Name = "Movie name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description es obligatorio")]
        [Display(Name = "Movie Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price in $")]
        [Display(Name = "Movie Price")]
        public double Price { get; set; }


        [Required(ErrorMessage = "Image postyer Url is required")]
        [Display(Name = "Movie Poster Url")]
        public string Image { get; set; }


        [Required(ErrorMessage = "Image postyer Url is required")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }


        [Required(ErrorMessage = "Movie category is required")]
        [Display(Name = "Select Movie category")]
        public Data.MovieCategory MovieCategory { get; set; }


        [Required(ErrorMessage = "Actor is required")]
        [Display(Name = "Select Actor for movie")]
        public List<int> ActorIds { get; set; }

        [Required(ErrorMessage = "cinema is required")]
        [Display(Name = "Select a cinema")]
        public int CinemaId { get; set; }


        [Required(ErrorMessage = "Producer is required")]
        [Display(Name = "Select a Producer")]
        public int ProducerId { get; set; }

    }
}
