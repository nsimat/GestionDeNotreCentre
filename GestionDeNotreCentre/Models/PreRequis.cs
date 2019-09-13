using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionDeNotreCentre.Models
{
    [Table("PREREQUIS")]
    public class PreRequis
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdModulePreRequis { get; set; }

        [Required]
        public int IdModule { get; set; }
    }
}