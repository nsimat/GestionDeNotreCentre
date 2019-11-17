using GestionDeNotreCentre.App.Messages;
using GestionDeNotreCentre.App.Utility;
using GestionDeNotreCentre.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestionDeNotreCentre.App
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Les champs

        public bool enabled;
        //private Window navigationMenu = System.Windows.Application.Current.MainWindow;

        #endregion

        #region Les ViewModels

        private ViewModelBase currentViewModel;
        private readonly LoginViewModel loginViewModel = new LoginViewModel();
        private readonly WelcomeScreenViewModel welcomeScreenViewModel = new WelcomeScreenViewModel();
        private readonly UserProfilViewModel userProfilViewModel = new UserProfilViewModel();
        private readonly PlanningViewModel planningViewModel = new PlanningViewModel();
        private readonly InscriptionViewModel inscriptionViewModel = new InscriptionViewModel();
        private readonly SettingsViewModel settingsViewModel = new SettingsViewModel();

        //Formateur
        private readonly FormateurViewModel formateurViewModel = new FormateurViewModel();
        private readonly FormateurDetailViewModel formateurDetailViewModel = new FormateurDetailViewModel();
        private readonly NewFormateurViewModel newFormateurViewModel = new NewFormateurViewModel();

        //Module
        private readonly ModuleViewModel moduleViewModel = new ModuleViewModel();
        private readonly ModuleDetailViewModel moduleDetailViewModel = new ModuleDetailViewModel();
        private readonly NewModuleViewModel newModuleViewModel = new NewModuleViewModel();

        //Formation
        private readonly FormationViewModel formationViewModel = new FormationViewModel();
        private readonly NewFormationViewModel newFormationViewModel = new NewFormationViewModel();
        private readonly FormationDetailViewModel formationDetailViewModel = new FormationDetailViewModel();

        //InstanceFormation
        private readonly InstanceFormationViewModel instanceFormationViewModel = new InstanceFormationViewModel();
        private readonly InstanceFormationDetailViewModel instanceFormationDetailViewModel = new InstanceFormationDetailViewModel();
        private readonly NewInstanceFormationViewModel newInstanceFormationViewModel = new NewInstanceFormationViewModel();

       
        #endregion

        #region Les Commandes

        public ICommand ExitCommand { get; set; }
        public ICommand AccountCommand { get; set; }   
        public ICommand NotificationCommand { get; set; }
        public ICommand NavigationCommand { get; private set; }//RelayCommand<string>
        
        #endregion

    

        #region Constructeur 

        public MainWindowViewModel()
        {
            StartApplication();
            NavigationCommand = new RelayCommand<string>(OnNavigation);
            IsLoggedIn = false;
        }

        #endregion

        #region Propriétés

        public ViewModelBase CurrentViewModel 
        {
            get 
            { 
                return currentViewModel; 
            }
            set 
            {
                currentViewModel = value;
                RaisePropertyChanged(nameof(CurrentViewModel));
            } 
        }

        public bool IsLoggedIn 
        {
            get 
            {
                return enabled;
            }
            set 
            {
                enabled = value;
                RaisePropertyChanged(nameof(IsLoggedIn));
            } 
        }

        #endregion

        #region Les méthodes

        private void StartApplication()
        {
            #region Lancement des commandes de la barre d'outils

            ExitCommand = new RelayCommand(ExitApplication, CanExitApplication);
            AccountCommand = new RelayCommand(DeconnectUser, CanDeconnectUser);//est-il necessaire si navigationbar peut le faire pour se déconnecter 
            NotificationCommand = new RelayCommand(DisplayNotification, CanDisplayNotification);


            #endregion

            CurrentViewModel = loginViewModel;
            //UIElement navBar = (UIElement)navigationMenu.FindName("NavBar");
            //navBar.Visibility = Visibility.Hidden;
            //Messenger.Default.Register<PersonneAuthenticatedMessage>(this, OnPersonneAuthenticatedMessage);
            //Messenger.Default.Register<ShowModuleDetailViewMessage>(this, OnShowModuleDetailViewMessageReceived);

            #region Enregistrement à certains messages émis

            //Login
            MyMessenger<PersonneAuthenticatedMessage>.Instance.Register(OnPersonneAuthenticatedMessage);

            //Module
            MyMessenger<ShowModuleDetailViewMessage>.Instance.Register(OnShowModuleDetailViewMessageReceived);
            MyMessenger<CreateNewModuleMessage>.Instance.Register(OnCreateModuleMessageReceived);
            MyMessenger<ShowModuleViewMessage>.Instance.Register(OnShowModuleViewMessageReceived);

            //Formation
            MyMessenger<ShowFormationDetailMessage>.Instance.Register(OnShowModuleDetailViewMessageReceived);
            MyMessenger<CreateNewFormationMessage>.Instance.Register(OnCreateModuleMessageReceived);
            MyMessenger<DisplayFormationViewMessage>.Instance.Register(OnDisplayFormationViewMessageReceived);

            //Formateur
            MyMessenger<ShowFormateurDetailMessage>.Instance.Register(OnShowFormateurDetailMessageReceived);
            MyMessenger<CreateNewFormateurMessage>.Instance.Register(OnCreateNewFormateurMessageReceived);

            //InstanceFormation
            MyMessenger<ShowInstanceFormationDetailMessage>.Instance.Register(OnShowInstanceFormationDetailMessageReceived);
            MyMessenger<CreateNewInstanceFormationMessage>.Instance.Register(OnCreateInstanceFormationMessageReceived);
            MyMessenger<DisplayInstanceFormationViewMessage>.Instance.Register(OnDisplayInstanceFormationViewMessageReceived);

            MyMessenger<DisplayPlanificationFromInstanceFormationMessage>.Instance.Register(OnDisplayPlanificationFromInstanceFormationMessageReceived);
            MyMessenger<DisplayInscriptionFromInstanceFormationMessage>.Instance.Register(OnDisplayInscriptionFromInstanceFormationMessageReceived);
            MyMessenger<ReturnToInstanceFormationsViewMessage>.Instance.Register(OnCancelNewInstanceFormationCreationMessageReceived);
            #endregion

        }

        private void OnShowModuleViewMessageReceived(ShowModuleViewMessage obj)
        {
            CurrentViewModel = moduleViewModel;
        }

        private void OnDisplayFormationViewMessageReceived(DisplayFormationViewMessage obj)
        {
            CurrentViewModel = formationViewModel;
        }

        private void OnCancelNewInstanceFormationCreationMessageReceived(ReturnToInstanceFormationsViewMessage obj)
        {
            CurrentViewModel = instanceFormationViewModel;
        }

        private void OnDisplayInscriptionFromInstanceFormationMessageReceived(DisplayInscriptionFromInstanceFormationMessage obj)
        {
            CurrentViewModel = inscriptionViewModel;//Detail???à modifier
        }

        private void OnDisplayPlanificationFromInstanceFormationMessageReceived(DisplayPlanificationFromInstanceFormationMessage obj)
        {
            CurrentViewModel = planningViewModel;//Detail???à modifier
        }

        private void OnDisplayInstanceFormationViewMessageReceived(DisplayInstanceFormationViewMessage obj)
        {
            CurrentViewModel = instanceFormationViewModel;
        }

        private void OnCreateInstanceFormationMessageReceived(CreateNewInstanceFormationMessage obj)
        {
            CurrentViewModel = newInstanceFormationViewModel;
        }

        private void OnShowInstanceFormationDetailMessageReceived(ShowInstanceFormationDetailMessage obj)
        {
            CurrentViewModel = instanceFormationDetailViewModel;
        }

        private void OnCreateNewFormateurMessageReceived(CreateNewFormateurMessage obj)
        {
            CurrentViewModel = newFormateurViewModel;
        }

        private void OnShowFormateurDetailMessageReceived(ShowFormateurDetailMessage obj)
        {
            CurrentViewModel = formateurDetailViewModel;
        }

        private void OnShowModuleDetailViewMessageReceived(ShowFormationDetailMessage obj)
        {
            CurrentViewModel = formationDetailViewModel;
        }

        private void OnCreateModuleMessageReceived(CreateNewFormationMessage obj)
        {
            CurrentViewModel = newFormationViewModel;
        }

        private void OnCreateModuleMessageReceived(CreateNewModuleMessage obj)
        {
            CurrentViewModel = newModuleViewModel;
        }

        private void OnShowModuleDetailViewMessageReceived(ShowModuleDetailViewMessage obj)
        {
            CurrentViewModel = moduleDetailViewModel;
        }

        private void DisplayNotification(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanDisplayNotification(object obj)
        {
            throw new NotImplementedException();
        }

        private void DeconnectUser(object obj)
        {
            CurrentViewModel = loginViewModel;//
        }

        private bool CanDeconnectUser(object obj)
        {
            return true;//mettre une fenêtre de dialogue qui demande s'il faut se déconnecter (true/false)
        }

        private bool CanExitApplication(object obj)
        {
            return true;
        }

        private void ExitApplication(object obj)
        {
            Application.Current.Shutdown();
        }

        private void OnPersonneAuthenticatedMessage(PersonneAuthenticatedMessage obj)
        {            
            welcomeScreenViewModel.WelcomePersonne = obj.Employe;
            CurrentViewModel = welcomeScreenViewModel;
            IsLoggedIn = obj.Authenticated;            
        }
        

        private void OnNavigation(string destination)
        {
            switch (destination)
            {
                case "login":
                    CurrentViewModel = loginViewModel;
                    break;
                case "welcome":
                    CurrentViewModel = welcomeScreenViewModel;
                    break;
                case "module":
                    CurrentViewModel = moduleViewModel;
                    break;
                case "moduledetail":
                    CurrentViewModel = moduleDetailViewModel;
                    break;
                case "formation":
                    CurrentViewModel = formationViewModel;
                    break;
                case "formateur":
                    CurrentViewModel = formateurViewModel;
                    break;
                case "userprofil":
                    CurrentViewModel = userProfilViewModel;
                    break;
                case "instanceformation":
                    CurrentViewModel = instanceFormationViewModel;
                    break;
                case "planification":
                    CurrentViewModel = planningViewModel;
                    break;
                case "inscription":
                    CurrentViewModel = inscriptionViewModel;
                    break;
                case "settings":
                    CurrentViewModel = settingsViewModel;
                    break;
                default:
                    CurrentViewModel = welcomeScreenViewModel;
                    break;
            }
        }

        #endregion

    }
}