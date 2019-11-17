using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace GestionDeCentreDAL.Models
{

    [Table("COMPOSITION")]
    public class Composition
    {

        [Required]
        [Display(Name ="Formation")]
        public int IdFormation { get; set; }

        [Required]
        [Display(Name ="Module")]
        public int IdModule { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateAjout { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateSuppression { get; set; }

        //Les clés étrangères
        public Module Module { get; set; }
        public Formation Formation { get; set; }

        public Composition From(IDataRecord dr)
        {
            return new Composition()
            {
                IdFormation = (int)dr["IdFormation"],
                IdModule = (int)dr["IdModule"],
                DateAjout = (DateTime)dr["DateAjout"],
                DateSuppression = (dr["DateSuppression"] is DBNull) ? null : (DateTime?)dr["DateSuppression"]                
            };
        }
    }
}