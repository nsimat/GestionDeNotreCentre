﻿using GestionDeCentreDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeNotreCentre.App.Messages
{
    public class EditFormationMessage
    {
        public Formation SelectedFormation { get; set; }
    }
}