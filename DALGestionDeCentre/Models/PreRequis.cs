using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace GestionDeCentreDAL.Models
{
    [Table("PREREQUIS")]
    public class PreRequis
    {        
        public int IdModulePreRequis { get; set; }
        public int IdModule { get; set; }

        #region Les clefs étrangères

        public Module ModulePreRequis { get; set; }        
        public Module Module { get; set; }

        #endregion

        public PreRequis From(IDataRecord dr)
        {
            return new PreRequis()
            {
                IdModulePreRequis = (int)dr["IdModulePrerequis"],
                IdModule = (int)dr["IdModule"]                
            };
        }
    }
}