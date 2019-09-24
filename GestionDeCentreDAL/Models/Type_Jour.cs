using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionDeCentreDAL.Models
{
    [Table("TYPE_JOUR")]
    public class Type_Jour
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTypeJour { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name ="Type de Jour")]
        public string TypeJour { get; set; }

        public ICollection<Planification> Planifications { get; set; }
    }
}