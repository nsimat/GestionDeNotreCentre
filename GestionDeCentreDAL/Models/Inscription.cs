using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionDeNotreCentre.Models
{
    [Table("INSCRIPTION")]
    public class Inscription
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdInscription { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Date d'inscription")]
        public DateTime DateInscription { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Date de validation")]
        public DateTime DateValidation { get; set; }

        [Required]
        [Display(Name ="Formation")]
        public int IdInstanceFormation { get; set; }
        public InstanceFormation InstanceFormation  { get; set; }

        [Required]
        [MaxLength(25), MinLength(11)]
        [Display(Name ="Stagiaire")]
        public string NumeroRegistreStatigiaire{ get; set; }
        public Personne Personne { get; set; }

        [Required]
        [MaxLength(25), MinLength(11)]
        [Display(Name ="Employé")]
        public string NumeroRegistreEmployes { get; set; }
        public Employe Employe { get; set; }


    }
}