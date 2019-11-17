using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace GestionDeCentreDAL.Models
{
    [Table("COMPETENCE")]
    public class Competence
    {
        [Required]
        [Display(Name = "Formateur")]
        public int IdFormateur { get; set; }

        [Required]
        [Display(Name ="Module")]
        public int IdModule { get; set; }

        #region Les clés étrangères

        public Formateur Formateur { get; set; }
        public Module Module { get; set; }

        #endregion

        public Competence From(IDataRecord dr)
        {
            return new Competence()
            {
                IdFormateur = (int)dr["IdFormateur"],
                IdModule = (int)dr["IdModule"]
            };
        }
    }
}