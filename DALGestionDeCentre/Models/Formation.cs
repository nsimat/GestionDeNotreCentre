using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace GestionDeCentreDAL.Models
{
    [Table("FORMATION")]
    public class Formation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFormation { get; set; }

        [StringLength(50, ErrorMessage = "Le nom de la formation doit avoir une longueur d'au plus 50 caractères.")]
        [Required]
        [Display(Name ="Formation")]
        public string Nom { get; set; }

        [StringLength(int.MaxValue)]
        [Required(ErrorMessage = "Une description doit avoir au minimum 3 caractères.")]
        [Display(Name = "Description de la Formation")]
        public string DescriptionFormation { get; set; }

        public ICollection<Composition> Compositions { get; set; }


        public Formation From(IDataRecord dr)
        {
            return new Formation()
            {
                IdFormation          = (int)dr["IdFormation"],
                Nom                  = (string)dr["Nom"],
                DescriptionFormation = (string)dr["DescriptionFormation"]
            };
        }

    }
}