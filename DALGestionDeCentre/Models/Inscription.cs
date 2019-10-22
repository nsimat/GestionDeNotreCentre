using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace GestionDeCentreDAL.Models
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
        public DateTime? DateValidation { get; set; }

        [Required]
        [Display(Name ="Formation")]
        public int IdInstanceFormation { get; set; }
        public InstanceFormation InstanceFormation  { get; set; }

        [Required]
        [MaxLength(25), MinLength(11)]
        [Display(Name ="Stagiaire")]
        public int IdStatigiaire{ get; set; }
        public Personne Personne { get; set; }

        [Required]
        [MaxLength(25), MinLength(11)]
        [Display(Name ="Employé")]
        public int IdEmploye { get; set; }
        public Employe Employe { get; set; }

        public Inscription From(IDataRecord dr)
        {
            return new Inscription()
            {
                IdInscription = (int)dr["IdInscription"],
                DateInscription = (DateTime)dr["DateInscription"],
                DateValidation = (dr["DateValidation"] is DBNull) ? null : (DateTime?)dr["DateValidation"],
                IdInstanceFormation = (int)dr["IdInstanceFormation"],
                IdStatigiaire = (int)dr["IdStagiaire"],
                IdEmploye = (int)dr["IdEmploye"]                
            };
        }


    }
}