using GestionDeCentreDAL.Models;
using GestionDeNotreCentre.App.Extensions;
using GestionDeNotreCentre.App.Messages;
using GestionDeNotreCentre.App.Services;
using GestionDeNotreCentre.App.Utility;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestionDeNotreCentre.App.ViewModels
{
    public class NewModuleViewModel : ViewModelBase
    {
        #region Les champs

        private readonly ModuleDataService moduleDataService;
        private readonly PreRequisDataService preRequisDataService;
        private string fileName;             //Le nom de fichier contenant la table des matières
        private Module newModule;            //Le module que l'on veut créer
        private Module selectedModule;       //Module sélectionné dans la liste des modules existants dans la table des modules => ajoutable
        private PreRequis selectedPreRequis; //Module sélectionné dans la liste des modules prérequis => supprimable
        private ObservableCollection<PreRequis> modulePreRequisites;
        private ObservableCollection<Module> modules;
        private byte[] uploadedFile;

        #endregion

        #region Les commandes

        public ICommand AddNewModuleCommand { get; set; }
        public ICommand CancelModuleCreationCommand { get; set; }
        public ICommand AddTableOfContentsFileCommand { get; set; }
        public ICommand AddPreRequisiteCommand { get; set; }
        public ICommand DeletePreRequisiteCommand { get; set; }

        #endregion

        #region Les propriétés

        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
                RaisePropertyChanged(nameof(FileName));
            }
        }

        public Module NewModule
        {
            get
            {
                return newModule;
            }
            set
            {
                selectedModule = value;
                RaisePropertyChanged(nameof(NewModule));
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

        public PreRequis SelectedPreRequis
        {
            get
            {
                return selectedPreRequis;
            }
            set
            {
                selectedPreRequis = value;
                RaisePropertyChanged(nameof(SelectedPreRequis));
            }
        }

        public ObservableCollection<PreRequis> ModulePreRequisites
        {
            get
            {
                return modulePreRequisites;
            }
            set
            {
                modulePreRequisites = value;
                RaisePropertyChanged(nameof(ModulePreRequisites));
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

        public NewModuleViewModel()
        {
            newModule = new Module();
            moduleDataService = new ModuleDataService();
            preRequisDataService = new PreRequisDataService();

            LoadModules();

            LoadCommands();            
        }

        #endregion

        #region Les méthodes        

        private void LoadModules()
        {
            Modules = moduleDataService.GetAllElements().ToObservableCollection();
        }

        private void LoadCommands()
        {
            AddNewModuleCommand = new RelayCommand(AddNewModule, CanAddNewModule);
            CancelModuleCreationCommand = new RelayCommand(CancelModuleCreation, CanCancelModuleCreation);
            AddTableOfContentsFileCommand = new RelayCommand(AddTableOfContentsFile, CanAddTableOfContentsFile);
            AddPreRequisiteCommand = new RelayCommand(AddPrerequisite, CanAddPreRequisite);
            DeletePreRequisiteCommand = new RelayCommand(DeletePreRequisite, CanDeletePrerequisite);
        }

        private void AddNewModule(object obj)
        {
            newModule.TableDeMatieres = uploadedFile;
            Module result = moduleDataService.CreateElement(newModule);


            if (result != null)
            {
                Module module = moduleDataService.GetModuleByName(result.Nom);
                newModule.IdModule = module.IdModule;

                if(newModule.PreRequis != null)
                {
                    foreach (var pr in newModule.PreRequis)
                    {
                        pr.IdModule = newModule.IdModule;

                        //Renvoie un objet PreRequis
                        preRequisDataService.CreateElement(pr);
                    }                    
                }

                MyMessenger<ShowModuleViewMessage>.Instance.Send(new ShowModuleViewMessage());
                MyMessenger<UpdateModulesListMessage>.Instance.Send(new UpdateModulesListMessage());
                newModule = new Module();
            }
                
        }

        private bool CanAddNewModule(object obj)
        {
            return true;
        }

        private void CancelModuleCreation(object obj)
        {
            MyMessenger<ShowModuleViewMessage>.Instance.Send(new ShowModuleViewMessage());
        }

        private bool CanCancelModuleCreation(object obj)
        {
            return true;
        }

        private void AddTableOfContentsFile(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Sélectionner la table des matières au format PDF...",
                Filter = "PDF files (*.PDF)|*.PDF",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            Nullable<bool> result = openFileDialog.ShowDialog();

            if (result == true)
            {
                FileName = openFileDialog.FileName;

                uploadedFile = File.ReadAllBytes(fileName);                
            }
        }

        private bool CanAddTableOfContentsFile(object obj)
        {
            return true;
        }

        private void DeletePreRequisite(object prerequis)
        {
            if (newModule.PreRequis != null)
            {
                newModule.PreRequis.Remove(selectedPreRequis);
            }

            ModulePreRequisites = newModule.PreRequis.ToObservableCollection();
        }

        private bool CanDeletePrerequisite(object obj)
        {
            if (selectedPreRequis != null)
                return true;
            return false;
        }

        private void AddPrerequisite(object obj)
        {
            //Vérifier si la liste des Prérequis du Module à créer est nulle. Sinon, la créer
            if (newModule.PreRequis == null)
            {
                newModule.PreRequis = new List<PreRequis>(); ;
            }

            if (!FindAmongPrerequisites(selectedModule))
            {
                PreRequis ourSelectedPreRequis = new PreRequis
                {
                    IdModule = newModule.IdModule,//vaut 0 à la création
                    Module = newModule,
                    IdModulePreRequis = SelectedModule.IdModule,
                    ModulePreRequis = SelectedModule
                };

                //Ajouter le Prerequis dans la liste des Prérequis du Module à créer
                newModule.PreRequis.Add(ourSelectedPreRequis);//les prerequis sont aussi ajoutés dans la liste des prérequis du module
                                                              
                ModulePreRequisites = newModule.PreRequis.ToObservableCollection();
            }                        
        }

        private bool CanAddPreRequisite(object obj)
        {
            if (SelectedModule != null)
                return true;
            return false;
        }    
        
        private bool FindAmongPrerequisites(Module md)
        {
            foreach(var pr in newModule.PreRequis)
            {
                if (pr.IdModulePreRequis == md.IdModule)
                    return true;
            }
            return false;
        }

        #endregion
    }
}
