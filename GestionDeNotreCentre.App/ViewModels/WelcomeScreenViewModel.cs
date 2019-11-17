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
        private Personne Employe { get; set; }

        public WelcomeScreenViewModel()
        {
            //Messenger.Default.Register<PersonneAuthenticatedMessage>(this, OnReceivedMessage);
            MyMessenger<PersonneAuthenticatedMessage>.Instance.Register(OnReceivedMessage);
        }

        private void OnReceivedMessage(PersonneAuthenticatedMessage obj)
        {
            Employe = obj.Employe;
        }

        public Personne WelcomePersonne
        {
            get
            {
                return Employe;
            }
            set 
            { 
                Employe = value;
                RaisePropertyChanged(nameof(WelcomePersonne));
            }
        }

        public string Welcome //à convertir via converter
        {
            get 
            {
                return "Bienvenu(e) " + Employe.Prenom + " " + Employe.Nom + " pour l'utilisation de notre application...";
            }              
        }
    }
}
