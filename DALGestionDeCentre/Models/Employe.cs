using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace GestionDeCentreDAL.Models
{
    
   [Table("EMPLOYE")]
    public class Employe
    {        
        public int IdEmploye { get; set; }

        public Personne Personne { get; set; }

        public Employe From(IDataRecord dr)
        {
            return new Employe()
            {
                IdEmploye = (int)dr["IdEmploye"]                
            };
        }

    }
}