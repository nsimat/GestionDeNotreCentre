using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace GestionDeCentreDAL.Models
{
    [Table("FORMATEUR")]
    public class Formateur
    {
        [Required]        
        public int IdFormateur { get; set; }


        #region Les clefs étrangères

        public Personne Personne { get; set; }
        public ICollection<Planification> Planifications { get; set; }
        public ICollection<Competence> Competences { get; set; }

        #endregion

        public Formateur From(IDataRecord dr)
        {
            return new Formateur()
            {
                IdFormateur = (int)dr["IdFormateur"]
            };
        }
    }    
}