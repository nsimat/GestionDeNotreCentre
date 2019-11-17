using GestionDeCentreDAL.Models;
using GestionDeNotreCentre.App.Extensions;
using GestionDeNotreCentre.App.Services;
using GestionDeNotreCentre.App.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestionDeNotreCentre.App.ViewModels
{
    public class NewFormationViewModel : ViewModelBase
    {
        #region Les champs

        private readonly FormationDataService formationDataService;
        private readonly ModuleDataService moduleDataService;

        private Formation newFormation;                  //La formation à créer
        private Module selectedModuleFromFormation;      //Module sélectionné dans la liste des modules de la formation => supprimable
        private Module selectedModuleFromTableOfModules; //Module sélectionné dans la liste des modules de la table des modules => ajoutable
        private ObservableCollection<Module> modulesOfFormation;
        private ObservableCollection<Module> modules;

        #endregion

        #region Les commandes

        public ICommand AddNewFormationCommand { get; set; }
        public ICommand CancelFormationCreationCommand { get; set; }        
        public ICommand AddModuleToFormationCommand { get; set; }
        public ICommand DeleteModuleFromFormationCommand { get; set; }

        #endregion

        #region Les propriétés

        public Formation NewFormation
        {
            get
            {
                return newFormation;
            }
            set
            {
                newFormation = value;
                RaisePropertyChanged(nameof(NewFormation));
            }
        }

        public Module SelectedModuleFromFormation
        {
            get
            {
                return selectedModuleFromFormation;
            }
            set
            {
                selectedModuleFromFormation = value;
                RaisePropertyChanged(nameof(SelectedModuleFromFormation));
            }
        }

        public Module SelectedModuleFromTableOfModules
        {
            get
            {
                return selectedModuleFromTableOfModules;
            }
            set
            {
                selectedModuleFromTableOfModules = value;
                RaisePropertyChanged(nameof(SelectedModuleFromTableOfModules));
            }
        }

        public ObservableCollection<Module> ModulesOfFormation
        {
            get
            {
                return modulesOfFormation;
            }
            set
            {
                modulesOfFormation = value;
                RaisePropertyChanged(nameof(ModulesOfFormation));
            }
        }

        public ObservableCollection<Module> Modules
        {
            get
            {
                return modules;
            }
            set
            {
                modules = value;
                RaisePropertyChanged(nameof(Modules));
            }
        }

        #endregion

        #region Le constructeur

        public NewFormationViewModel()
        {
            newFormation = new Formation
            {
                Compositions = new List<Composition>()
            };
            moduleDataService = new ModuleDataService();
            formationDataService = new FormationDataService();

            LoadModules();

            LoadModulesOfFormation();

            LoadCommands();


        }

        #endregion

        #region Les méthodes        

        private void LoadModules()
        {
            Modules = moduleDataService.GetAllElements().ToObservableCollection();            
        }

        private void LoadModulesOfFormation()
        {
            var formationCompositions = newFormation.Compositions;
            var formationModules = new List<Module>();

            foreach(var composition in formationCompositions)
            {
                formationModules.Add(composition.Module);
            }
            ModulesOfFormation = formationModules.ToObservableCollection();
        }
        private void LoadCommands()
        {
            AddNewFormationCommand = new RelayCommand(AddNewFormation, CanAddNewFormation);
            CancelFormationCreationCommand = new RelayCommand(CancelFormationCreation, CanCancelFormationCreation);
            AddModuleToFormationCommand = new RelayCommand(AddModule, CanAddModule);
            DeleteModuleFromFormationCommand = new RelayCommand(DeleteModule, CanDeleteModule);
        }

        private void AddNewFormation(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanAddNewFormation(object obj)
        {
            throw new NotImplementedException();
        }

        private void CancelFormationCreation(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanCancelFormationCreation(object obj)
        {
            throw new NotImplementedException();
        }

        private void AddModule(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanAddModule(object obj)
        {
            if (selectedModuleFromTableOfModules != null)
                return true;
            return false;
        }

        private void DeleteModule(object obj)
        {
            //if()
        }

        private bool CanDeleteModule(object obj)
        {
            if (selectedModuleFromFormation != null)
                return true;
            return false;
        }

        #endregion
    }
}
