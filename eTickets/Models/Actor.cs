using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Actor: IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display( Name = "Profile Picture URL")]
        [Required(ErrorMessage ="El campo imagen es Obligatorio")]
        public string ProfilePictureURL { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "El campo Nombre es Obligatorio")]
        [StringLength(50, MinimumLength = 3 , ErrorMessage = "El nombre debe estar entre 3 y 50 caracteres")]
        public string  FullName { get; set; }

        [Display(Name = "Biografy")]
        public string Bio { get; set; }


        //Relacionnes
        public List<Actor_Movie> Actors_Movies { get; set; }

    }
}
