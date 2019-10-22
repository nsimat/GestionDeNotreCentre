using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace GestionDeCentreDAL.Models
{
    [Table("INSTANCEFORMATION")]
    public class InstanceFormation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdInstanceFormation { get; set; }

        [StringLength(25)]
        [Required]
        [Display(Name ="Statut de la formation")]
        public string Statut { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Date de début")]
        public DateTime DateDebut { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Date de fin")]
        public DateTime DateFin { get; set; }

        [Required]
        [Display(Name ="Formation")]
        public int IdFormation { get; set; }
        public Formation Formation { get; set; }

        [Required]
        [Display(Name ="Employé")]
        public int IdEmploye { get; set; }
        public Employe Employe { get; set; }

        //Clefs étrangères
        public ICollection<Inscription> Inscriptions { get; set; }
        public ICollection<Planification> Planifications { get; set; }


        public InstanceFormation From(IDataRecord dr)
        {
            return new InstanceFormation()
            {
                IdInstanceFormation = (int)dr["IdInstanceFormation"],
                Statut = (string)dr["Statut"],
                DateDebut = (DateTime)dr["DateDebut"],
                DateFin = (DateTime)dr["DateFin"],
                IdFormation = (int)dr["IdFormation"],
                IdEmploye = (int)dr["IdPersonne"]
            };

        }
    }
}