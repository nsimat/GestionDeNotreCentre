using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestionDeNotreCentre.App.Services
{
    public class LoginDialogService : IDialogService
    {
        Window dashboardView = null;

        public void CloseDialog()
        {
           MessageBox.Show("Let's close the Windows!");
            if (dashboardView != null)
                dashboardView.Close();
        }

        public void ShowDialog()
        {
            MessageBox.Show("Let's pass to the next view!");// à supprimer
            dashboardView = new MainWindow();
            dashboardView.ShowDialog();            
        }
    }
}
