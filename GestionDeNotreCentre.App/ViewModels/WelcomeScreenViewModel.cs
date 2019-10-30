using GestionDeCentreDAL.Models;
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
    public class WelcomeScreenViewModel : ViewModelBase
    {
        private Personne _employe { get; set; }

        public WelcomeScreenViewModel()
        {
            Messenger.Default.Register<PersonneAuthenticatedMessage>(this, OnReceivedMessage);
        }

        private void OnReceivedMessage(PersonneAuthenticatedMessage obj)
        {
            _employe = obj.Employe;
        }

        public Personne WelcomePersonne
        {
            get
            {
                return _employe;
            }
            set 
            { 
                _employe = value;
                RaisePropertyChanged(nameof(WelcomePersonne));
            }
        }

        public string Welcome //à convertir via converter
        {
            get 
            {
                return "Bienvenu(e)" + _employe.Prenom + " " + _employe.Nom + " pour l'utilisation de notre application...";
            }              
        }
    }
}
