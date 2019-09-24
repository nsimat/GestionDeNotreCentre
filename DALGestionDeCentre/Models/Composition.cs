﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DateTime DateSuppression { get; set; }
    }
}