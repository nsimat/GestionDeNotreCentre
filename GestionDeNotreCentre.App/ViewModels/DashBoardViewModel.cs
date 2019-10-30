using GestionDeNotreCentre.App.Messages;
using GestionDeNotreCentre.App.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GestionDeNotreCentre.App.ViewModels
{
    public class DashBoardViewModel : ViewModelBase
    {

        public int MyProperty { get; set; }

        public DashBoardViewModel()
        {

            Messenger.Default.Register<CloseLoginWindowMessage>(this, OnClosingLoginreceived);
        }

        private void OnClosingLoginreceived(CloseLoginWindowMessage obj)
        {
            throw new NotImplementedException();
        }
       
    }
}
