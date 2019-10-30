using GestionDeNotreCentre.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeNotreCentre.App
{
    public class ViewModelLocator
    {
        private static readonly LoginViewModel loginViewModel = new LoginViewModel();

        public static LoginViewModel LoginViewModel { get => loginViewModel; }
    }
}
