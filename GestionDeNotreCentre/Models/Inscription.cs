using System;
using System.Collections.Generic;
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
        public DateTime DateInscription { get; set; }

        [Required]
        [DataType(DataType.Dare)]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}", ApplicationIdentity = true)]
        public DateTime DateValidation { get; set; }

       // public int IdInstanceFormation { get; set; }
        public InstanceFormation InstanceFormation  { get; set; }

        public NumeroRegistreStatigiaire NumeroRegistreStatigiaire{ get; set; }

        public ICollection<NumeroRegistreEmploye> numeroRegistreEmployes { get; set; }


    }
}