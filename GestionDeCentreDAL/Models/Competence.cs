using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionDeCentreDAL.Models
{
    [Table("COMPETENCE")]
    public class Competence
    {
        [Required]
        [StringLength(25, ErrorMessage ="Le nom de la compétence doit avoir au plus 25 caractères.")]
        public string NumeroRegistre { get; set; }

        [Required]
        [Display(Name ="Module")]
        public int IdModule { get; set; }
    }
}