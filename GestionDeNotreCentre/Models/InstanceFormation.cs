using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionDeNotreCentre.Models
{
    [Table("INSTANCEFORMATION")]
    public class InstanceFormation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdInstanceformation { get; set; }

        [StringLength(25)]
        [Required]
        public string Statut { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateDebut { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateFin { get; set; }

        [Required]
        public int IdFormation { get; set; }

        [Required]
        public string NumeroRegistre { get; set; }

        //Clefs étrangères
        //public ICollection<Inscription> Inscriptions { get; set; }
        public ICollection<Planification> Planifications { get; set; }
    }
}