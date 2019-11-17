using GestionDeNotreCentre.App.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestionDeNotreCentre.App.Services
{
    public class ModuleDialogservice : IDialogService
    {
        Window moduleDetailView = null;

        public ModuleDialogservice()
        {
            //moduleDetailView = new ModuleDetailView();
        }

        public void CloseDialog()
        {
            throw new NotImplementedException();
        }

        public void ShowDialog()
        {
            throw new NotImplementedException();
        }
    }
}
