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
    public class NewFormateurViewModel : ViewModelBase
    {
        #region Les champs

        private readonly FormateurDataService formateurDataService;
        private readonly ModuleDataService moduleDataService;
        private readonly CompetenceDataService competenceDataService;
        private readonly PersonneDataService personneDataService;

        private Formateur newFormateur;                       //Le formateur dont on affiche les détails
        private Module selectedModule;                        //Module sélectionné dans la liste des modules existants dans la table des modules => ajoutable
        private Competence selectedCompetence;                //Compétence (Module) sélectionnée dans la liste des compétences du formateur => supprimable  
        private ObservableCollection<Competence> competences; //Liste des modules qui composent la liste des compétences du formateur
        private ObservableCollection<Module> modules;         //Liste des modules existants dans la table des modules

        #endregion

        #region Les commandes

        public ICommand SaveFormateurCommand { get; set; }
        public ICommand CancelFormateurCreationCommand { get; set; }

        public ICommand AddCompetenceToFormateurCommand { get; set; }
        public ICommand DeleteModuleFromCompetenceCommand { get; set; }

        #endregion

        #region Les propriétés

        public Formateur NewFormateur
        {
            get
            {
                return newFormateur;
            }
            set
            {
                newFormateur = value;
                RaisePropertyChanged(nameof(NewFormateur));
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

        public Competence SelectedCompetence
        {
            get
            {
                return selectedCompetence;
            }
            set
            {
                selectedCompetence = value;
                RaisePropertyChanged(nameof(SelectedCompetence));
            }
        }

        public ObservableCollection<Competence> Competences
        {
            get
            {
                return competences;
            }
            set
            {
                competences = value;
                RaisePropertyChanged(nameof(Competences));
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

        public NewFormateurViewModel()
        {
            newFormateur = new Formateur();
            formateurDataService = new FormateurDataService();
            moduleDataService = new ModuleDataService();
            competenceDataService = new CompetenceDataService();
            personneDataService = new PersonneDataService();

            LoadModules();

            LoadCommands();
                        

        }

        #endregion

        #region Les méthodes

        private void LoadCommands()
        {
            SaveFormateurCommand = new RelayCommand(SaveFormateur, CanSaveFormateur);
            CancelFormateurCreationCommand = new RelayCommand(CancelFormateurCreation, CanCancelFormateurCreation);

            AddCompetenceToFormateurCommand = new RelayCommand(AddCompetenceToFormateur, CanAddCompetenceToFormateur);
            DeleteModuleFromCompetenceCommand = new RelayCommand(DeleteModuleFromCompetence, CanDeleteModulefromCompetence);
    }

        private void SaveFormateur(object obj)
        {
            formateurDataService.CreateElement(newFormateur);
        }

        private bool CanSaveFormateur(object obj)
        {
            return true;
        }

        private void CancelFormateurCreation(object obj)
        {
            formateurDataService.DeleteElement(newFormateur);
        }

        private bool CanCancelFormateurCreation(object obj)
        {
            return true;
        }

        private void AddCompetenceToFormateur(object obj)
        {
            Competence competence = competenceDataService.GetElementDetail(selectedModule.IdModule);
            if(Competences == null)
            {
                Competences = new List<Competence>().ToObservableCollection();
            }
            Competences.Add(competence); 
        }

        private bool CanAddCompetenceToFormateur(object obj)
        {
            if (selectedModule != null)
                return true;
            return false;
        }

        private void DeleteModuleFromCompetence(object obj)
        {
            if(competences != null)
            {
                Competences.Remove(selectedCompetence);
            }
        }

        private bool CanDeleteModulefromCompetence(object obj)
        {
            if (selectedCompetence != null)
                return true;
            return false;
        }

        private void LoadModules()
        {
            Modules = moduleDataService.GetAllElements().ToObservableCollection();
        }

        #endregion
    }
}
