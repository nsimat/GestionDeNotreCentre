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
        #region Déclaration des ViewModels

        private static readonly MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
        private static readonly LoginViewModel loginViewModel = new LoginViewModel();
        private static readonly WelcomeScreenViewModel welcomeScreenViewModel = new WelcomeScreenViewModel();
        private static readonly UserProfilViewModel userProfilViewModel = new UserProfilViewModel();
        private static readonly PlanningViewModel planningViewModel = new PlanningViewModel();
        private static readonly InscriptionViewModel inscriptionViewModel = new InscriptionViewModel();
        private static readonly SettingsViewModel settingsViewModel = new SettingsViewModel();

        private static readonly ModuleViewModel moduleViewModel = new ModuleViewModel();
        private static readonly ModuleDetailViewModel moduleDetailViewModel = new ModuleDetailViewModel();
        private static readonly NewModuleViewModel newModuleViewModel = new NewModuleViewModel();

        private static readonly FormationViewModel formationViewModel = new FormationViewModel();
        private static readonly FormationDetailViewModel formationDetailViewModel = new FormationDetailViewModel();
        private static readonly NewFormationViewModel newFormationViewModel = new NewFormationViewModel();
        
        private static readonly FormateurViewModel formateurViewModel = new FormateurViewModel();
        private static readonly FormateurDetailViewModel formateurDetailViewModel = new FormateurDetailViewModel();
        private static readonly NewFormateurViewModel newFormateurViewModel = new NewFormateurViewModel();

        private static readonly InstanceFormationViewModel instanceFormationViewModel = new InstanceFormationViewModel();
        private static readonly InstanceFormationDetailViewModel instanceFormationDetailViewModel = new InstanceFormationDetailViewModel();
        private static readonly NewInstanceFormationViewModel newInstanceFormationViewModel = new NewInstanceFormationViewModel();

        #endregion

        #region Les propriétés permettant d'obtenir un ViewModel

        public static MainWindowViewModel MainWindowViewModel { get => mainWindowViewModel; }
        public static LoginViewModel LoginViewModel { get => loginViewModel; }
        public static WelcomeScreenViewModel WelcomeScreenViewModel { get => welcomeScreenViewModel; }
        public static UserProfilViewModel UserProfilViewModel { get => userProfilViewModel; }
        public static PlanningViewModel PlanningViewModel { get => planningViewModel; }
        public static InscriptionViewModel InscriptionViewModel { get => inscriptionViewModel; }
        public static SettingsViewModel SettingsViewModel { get => settingsViewModel; }

        public static ModuleViewModel ModuleViewModel { get => moduleViewModel; }
        public static ModuleDetailViewModel ModuleDetailViewModel { get => moduleDetailViewModel; }
        public static NewModuleViewModel NewModuleViewModel { get => newModuleViewModel; }

        public static FormationViewModel FormationViewModel { get => formationViewModel; }
        public static FormationDetailViewModel FormationDetailViewModel { get => formationDetailViewModel; }
        public static NewFormationViewModel NewFormationViewModel { get => newFormationViewModel; }
        
        public static FormateurViewModel FormateurViewModel { get => formateurViewModel; }
        public static FormateurDetailViewModel FormateurDetailViewModel { get => formateurDetailViewModel; }
        public static NewFormateurViewModel NewFormateurViewModel { get => newFormateurViewModel; }

        public static InstanceFormationViewModel InstanceFormationViewModel { get => instanceFormationViewModel; }
        public static InstanceFormationDetailViewModel InstanceFormationDetailViewModel { get => instanceFormationDetailViewModel; }
        public static NewInstanceFormationViewModel NewInstanceFormationViewModel { get => newInstanceFormationViewModel; }


        #endregion

    }
}
