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
    public class FormationDetailViewModel : ViewModelBase
    {
        #region Les champs

        private readonly FormationDataService formationDataService;
        private readonly ModuleDataService moduleDataService;
        private readonly CompositionDataService compositionDataService;
        private readonly InstanceFormationDataService instanceFormationDataService;
        private Formation selectedFormation;                   //La formation dont on affiche les détails
        private Module selectedModule;                         //Module sélectionné dans la liste des modules existants dans la table des modules => ajoutable
        private Module selectedModuleComponent;                //Module sélectionné dans la liste des modules composant la formation => supprimable  
        private ObservableCollection<Module> moduleComponents; //Liste des modules qui composent la formation
        private ObservableCollection<Module> modules;          //Liste des modules existants dans la table des modules

        #endregion

        #region Les commandes

        public ICommand SaveFormationCommand { get; set; }
        public ICommand DeleteFormationCommand { get; set; }
        
        public ICommand AddModuleToFormationCommand { get; set; }
        public ICommand DeleteModuleFromFormationCommand { get; set; }

        #endregion

        #region Les propriétés

        public Formation SelectedFormation
        {
            get
            {
                return selectedFormation;
            }
            set
            {
                selectedFormation = value;
                RaisePropertyChanged(nameof(SelectedFormation));
            }
        }

        public Module SelectedModule
        {
            get
            {
                return selectedModule;
            }
            set
            {
                selectedModule = value;
                RaisePropertyChanged(nameof(SelectedModule));
            }
        }

        public Module SelectedModuleComponent
        {
            get
            {
                return selectedModuleComponent;
            }
            set
            {
                selectedModuleComponent = value;
                RaisePropertyChanged(nameof(SelectedModuleComponent));
            }
        }

        public ObservableCollection<Module> ModuleComponents
        {
            get
            {
                return moduleComponents;
            }
            set
            {
                moduleComponents = value;
                RaisePropertyChanged(nameof(ModuleComponents));
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

        public FormationDetailViewModel()
        {
            MyMessenger<EditFormationMessage>.Instance.Register(OnEditFormationMessageReceived);

            formationDataService = new FormationDataService();
            moduleDataService = new ModuleDataService();
            compositionDataService = new CompositionDataService();
            instanceFormationDataService = new InstanceFormationDataService();

            LoadModules();            

            LoadCommands();
        }

        #endregion

        #region Les méthodes        

        private void OnEditFormationMessageReceived(EditFormationMessage obj)
        {
            //Obtenir la formation sélectionnée
            SelectedFormation = obj.SelectedFormation;

            //Trouver la liste des compositions relative à cette formation
            selectedFormation.Compositions = compositionDataService.GetCompositionsFrom(selectedFormation.IdFormation);

            //Trouver la liste des instances de cette formation
            selectedFormation.InstanceFormations = instanceFormationDataService.GetAllElements();

            //Initialiser les listes de modules composant la formation
            moduleComponents = new List<Module>().ToObservableCollection(); 

            //Initialiser les modules de la formation
            var formationCompositions = SelectedFormation.Compositions;

            foreach (var composition in formationCompositions)
            {
                composition.Module = moduleDataService.GetElementDetail(composition.IdModule);
                moduleComponents.Add(composition.Module);
            }

            //Recharger la liste des modules dispensés dans le centre
            LoadModules();
        }        

        private void LoadModules()
        {
            Modules = moduleDataService.GetAllElements().ToObservableCollection();
        }

        private void LoadCommands()
        {
            SaveFormationCommand = new RelayCommand(SaveFormation, CanSaveFormation);
            DeleteFormationCommand = new RelayCommand(DeleteFormation, CanDeleteFormation);
            AddModuleToFormationCommand = new RelayCommand(AddModuleToFormation, CanAddModuleToFormation);
            DeleteModuleFromFormationCommand = new RelayCommand(DeleteModuleFromFormation, CanDeleteModuleFromFormation);
        }

        private void SaveFormation(object obj)
        {
            formationDataService.UpdateElement(selectedFormation);
            MyMessenger<DisplayFormationViewMessage>.Instance.Send(new DisplayFormationViewMessage());
        }

        private bool CanSaveFormation(object obj)
        {
            return true;
        }

        private void DeleteFormation(object obj)
        {
            //Supprimer les clés etrangères istanceformation et composition
            if(selectedFormation.Compositions != null)
            {
                foreach(var cmp in selectedFormation.Compositions)
                {
                    compositionDataService.DeleteElement(cmp);//à suivre
                }
            }

            if(selectedFormation.InstanceFormations != null)
            {
                foreach(var instance in selectedFormation.InstanceFormations)
                {
                    instanceFormationDataService.DeleteElement(instance);//à suivre
                }
            }

            //Supprimer la formation après que toutes les clefs étrangères soient supprimées
            bool result = formationDataService.DeleteElement(selectedFormation);

            //Retourner vers la vue générale des formation
            if (result)
                MyMessenger<DisplayFormationViewMessage>.Instance.Send(new DisplayFormationViewMessage());
        }

        private bool CanDeleteFormation(object obj)
        {
            return true;
        }

        private void AddModuleToFormation(object obj)
        {
            //Il faut ajouter module dans la table de Composition se rapportant à cette formation!!! => à revoir
            if(selectedFormation.Compositions == null)
            {
                selectedFormation.Compositions = new List<Composition>();
            }

            //Vérifier si le module ne se trouve pas déjà sur la liste des modules qui forment la formation
            if (!FindModuleFromModulesOfFormation(selectedModule))
            {
                //Créer une composition pour la formation
                Composition composition = new Composition
                {
                    IdFormation = selectedFormation.IdFormation,
                    IdModule = selectedModule.IdModule,
                    Formation = selectedFormation,
                    Module = moduleDataService.GetElementDetail(selectedModule.IdModule),
                    DateAjout = DateTime.Today//Date de création
                };

                Composition result = compositionDataService.CreateElement(composition);
                if (result != null)
                {
                    selectedFormation.Compositions.Add(composition);
                    moduleComponents.Add(selectedModule);
                }
            }          
            //ModuleComponents = moduleComponents;
            
        }

        private bool CanAddModuleToFormation(object obj)
        {
            if (selectedModule != null)
                return true;
            return false;
        }

        private bool FindModuleFromModulesOfFormation(Module module)
        {
            if (selectedFormation.Compositions.Count != 0)
            {
                foreach (var cmp in selectedFormation.Compositions)
                {
                    if (cmp.IdModule == module.IdModule)
                        return true;
                }
            }
            return false;
        }

        private void DeleteModuleFromFormation(object obj)
        {
            //Supprimer la composition de la table de composition
            Composition composition = new Composition
            {
                IdFormation = selectedFormation.IdFormation,
                IdModule = selectedModuleComponent.IdModule
            };

            compositionDataService.DeleteElement(composition);
            moduleComponents.Remove(selectedModuleComponent);
        }

        private bool CanDeleteModuleFromFormation(object obj)
        {
            if (selectedModuleComponent != null)
                return true;
            return false;
        }

        #endregion
    }
}
