using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionDeNotreCentre.Models
{

    [Table("COMPOSITION")]
    public class Composition
    {

        [Required]
        
        public int IdFormation { get; set; }

        [Required]
        public int IdModule { get; set; }



        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateAjout { get; set; }

        [Required]
        [DataType(DataType.Dare)]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}", ApplicationIdentity = true)]
        public DateTime DateSuppression { get; set; }
    }
}