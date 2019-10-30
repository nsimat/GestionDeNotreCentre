using GestionDeNotreCentre.App.Messages;
using GestionDeNotreCentre.App.Utility;
using GestionDeNotreCentre.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GestionDeNotreCentre.App
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly LoginViewModel _loginViewModel = new LoginViewModel();
        private readonly WelcomeScreenViewModel _welcomeScreenViewModel = new WelcomeScreenViewModel();
        private readonly ModuleViewModel _moduleViewModel = new ModuleViewModel();

        private ViewModelBase _currentViewModel;

        public MainWindowViewModel()
        {
            StartApp();
            NavigationCommand = new RelayCommand<string>(OnNavigation);
        }

        public ViewModelBase CurrentViewModel 
        {
            get 
            { 
                return _currentViewModel; 
            }
            set 
            {
                _currentViewModel = value;
                RaisePropertyChanged(nameof(CurrentViewModel));
            } 
        }

        private void StartApp()
        {
            CurrentViewModel = _loginViewModel;
            Messenger.Default.Register<PersonneAuthenticatedMessage>(this, OnPersonneAuthenticatedMessage);
        }

        private void OnPersonneAuthenticatedMessage(PersonneAuthenticatedMessage obj)
        {
            _welcomeScreenViewModel.WelcomePersonne = obj.Employe;
            CurrentViewModel = _welcomeScreenViewModel;
        }

        public RelayCommand<string> NavigationCommand { get; private set; }

        private void OnNavigation(string destination)
        {
            switch (destination)
            {
                case "login":
                    CurrentViewModel = _loginViewModel;
                    break;
                case "welcome":
                    CurrentViewModel = _welcomeScreenViewModel;
                    break;
                case "modules":
                    CurrentViewModel = _moduleViewModel;
                    break;
                default:
                    break;
            }
        }
       
    }
}
