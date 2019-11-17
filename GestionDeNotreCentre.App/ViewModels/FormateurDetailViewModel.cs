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
    public class FormateurDetailViewModel : ViewModelBase
    {
        #region Les champs

        private readonly FormateurDataService formationDataService;
        private readonly ModuleDataService moduleDataService;
        private readonly CompetenceDataService competenceDataService;

        private Formateur selectedFormateur;                  //Le formateur dont on affiche les détails
        private Module selectedModule;                        //Module sélectionné dans la liste des modules existants dans la table des modules => ajoutable
        private Module selectedCompetence;                    //Module sélectionné dans la liste des modules composant la formation => supprimable  
        private ObservableCollection<Competence> competences; //Liste des modules qui composent la liste des compétences du formateur
        private ObservableCollection<Module> modules;         //Liste des modules existants dans la table des modules

        #endregion

        #region Les commandes

        public ICommand SaveFormateurCommand { get; set; }
        public ICommand DeleteFormateurCommand { get; set; }

        public ICommand AddCompetenceToFormateurCommand { get; set; }
        public ICommand DeleteModuleFromCompetenceCommand { get; set; }

        #endregion

        #region Les propriétés

        public Formateur SelectedFormateur
        {
            get
            {
                return selectedFormateur;
            }
            set
            {
                selectedFormateur = value;
                RaisePropertyChanged(nameof(SelectedFormateur));
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

        public Module SelectedCompetence
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

        public FormateurDetailViewModel()
        {
            formationDataService = new FormateurDataService();
            moduleDataService = new ModuleDataService();
            competenceDataService = new CompetenceDataService();

            LoadModules();

            LoadCommands();

            MyMessenger<EditFormateurMessage>.Instance.Register(OnEditFormateurMessageReceived);

        }



        #endregion

        #region Les méthodes

        private void OnEditFormateurMessageReceived(EditFormateurMessage obj)
        {
            SelectedFormateur = obj.SelectedFormateur;
            //à compléter
        }

        private void LoadModules()
        {
            Modules = moduleDataService.GetAllElements().ToObservableCollection();
        }

        private void LoadCommands()
        {
            SaveFormateurCommand = new RelayCommand(SaveFormateur, CanSaveFormateur);
            DeleteFormateurCommand = new RelayCommand(DeleteFormateur, CanDeleteFormateur);
            AddCompetenceToFormateurCommand = new RelayCommand(AddCompetenceToFormateur, CanAddCompetenceToFormateur);
            DeleteModuleFromCompetenceCommand = new RelayCommand(DeleteModuleFromCompetence, CanDeleteModuleFromCompetence);
        }

        private void SaveFormateur(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanSaveFormateur(object obj)
        {
            throw new NotImplementedException();
        }

        private void DeleteFormateur(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanDeleteFormateur(object obj)
        {
            throw new NotImplementedException();
        }

        private void AddCompetenceToFormateur(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanAddCompetenceToFormateur(object obj)
        {
            throw new NotImplementedException();
        }

        private void DeleteModuleFromCompetence(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanDeleteModuleFromCompetence(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
