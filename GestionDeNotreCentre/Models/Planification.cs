using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionDeNotreCentre.Models
{
    [Table("PLANIFICATION")]
    public class Planification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPlanification { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Date")]
        public DateTime DatePlanification { get; set; }

        [Required]
        [Display(Name ="Type de jour")]
        public int IdTypeJour { get; set; }
        public Type_Jour Type_Jour { get; set; }

        [Required]
        [Display(Name ="Formateur")]
        public string NumeroRegistre { get; set; }
        public Formateur Formateur { get; set; }

        [Required]
        [Display(Name ="Formation")]
        public int IdInstanceformation { get; set; }
        public InstanceFormation InstanceFormation { get; set; }

        [Required]
        [Display(Name ="Module")]
        public int IdModule { get; set; }
    }
}