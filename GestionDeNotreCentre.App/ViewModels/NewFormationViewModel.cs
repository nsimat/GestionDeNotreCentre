using GestionDeCentreDAL.Models;
using GestionDeNotreCentre.App.Extensions;
using GestionDeNotreCentre.App.Messages;
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
        private readonly CompositionDataService compositionDataService;

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
                Compositions = new List<Composition>(),
                InstanceFormations = new List<InstanceFormation>()
            };

            moduleDataService = new ModuleDataService();
            formationDataService = new FormationDataService();
            compositionDataService = new CompositionDataService();

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
            var formationCompositions = newFormation.Compositions;//?vide en cas de new Formation
            var formationModules = new List<Module>();

            //????
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
            //Ajouter finalement la formation dans la table de Formation

            Formation result = formationDataService.CreateElement(newFormation);
            
            if(result != null)
            {
                Formation formation = formationDataService.GetFormationByName(result.Nom);

                //Ajouter ensuite les compositions

                foreach (var module in modulesOfFormation)
                {
                    Composition composition = new Composition
                    {
                        IdFormation = formation.IdFormation,
                        IdModule = module.IdModule,
                        DateAjout = DateTime.Today
                    };
                    compositionDataService.CreateElement(composition);
                }
                MyMessenger<UpdateFormationsListMessage>.Instance.Send(new UpdateFormationsListMessage());
                MyMessenger<DisplayFormationViewMessage>.Instance.Send(new DisplayFormationViewMessage());
            }           
            
        }

        private bool CanAddNewFormation(object obj)
        {
            return true;
        }

        private void CancelFormationCreation(object obj)
        {
            //Retourner vers la vue générale 
            MyMessenger<DisplayFormationViewMessage>.Instance.Send(new DisplayFormationViewMessage());
        }

        private bool CanCancelFormationCreation(object obj)
        {
            return true;
        }

        private void AddModule(object obj)
        {
            //Vérifier si le module ne se trouve pas déjà sur la liste des modules qui forment la formation
            if (!FindModuleFromModulesOfFormation(selectedModuleFromTableOfModules))
            {
                //Créer une composition pour la formation
                Composition composition = new Composition
                {                    
                    IdModule = selectedModuleFromTableOfModules.IdModule,
                    Formation = newFormation,
                    Module = selectedModuleFromTableOfModules,
                    DateAjout = DateTime.Today//Date de création
                };

                //Ajouter le module dans la liste des modules de l'objet NewFormation
                newFormation.Compositions.Add(composition);

                //Ajouter le module dans la liste des modules de la formation
                modulesOfFormation.Add(selectedModuleFromTableOfModules);                
            }

        }

        private bool CanAddModule(object obj)
        {
            if (selectedModuleFromTableOfModules != null)
                return true;
            return false;
        }

        private bool FindModuleFromModulesOfFormation(Module module)
        {
            if(modulesOfFormation.Count != 0)
            {
                foreach(var md in modulesOfFormation)
                {
                    if (md.IdModule == module.IdModule)
                        return true;
                }
            }
            return false;
        }

        private void DeleteModule(object obj)
        {
            //Supprimer le module de la liste des modules composant la formation
            Composition composition = new Composition
            {
                IdModule = selectedModuleFromFormation.IdModule,
                Formation = newFormation,
                Module = selectedModuleFromFormation,
                DateAjout = DateTime.Today//Date de création
            };
            newFormation.Compositions.Remove(composition);

            modulesOfFormation.Remove(selectedModuleFromFormation);
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
