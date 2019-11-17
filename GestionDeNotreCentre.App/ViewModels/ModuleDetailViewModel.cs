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
    public class ModuleDetailViewModel : ViewModelBase
    {
        #region Les champs

        private readonly ModuleDataService moduleDataService;
        private readonly PreRequisDataService preRequisDataService;
        private string fileName;             //Le nom de fichier contenant la table des matières
        private Module selectedModule;       //Le module dont on affiche les détails
        private Module selectedModuleLocal;  //Module sélectionné dans la liste des modules existants dans la table des modules => ajoutable
        private PreRequis selectedPreRequis; //Module sélectionné dans la liste des modules prérequis => supprimable
        private ObservableCollection<PreRequis> modulePreRequisites;
        private ObservableCollection<Module> modules;
        private byte[] uploadedFile;

        #endregion

        #region Les commandes

        public ICommand SaveModuleCommand { get; set; }
        public ICommand DeleteModuleCommand { get; set; }
        public ICommand UploadFileCommand { get; set; }
        public ICommand AddPreRequisCommand { get; set; }
        public ICommand DeletePreRequisCommand { get; set; }

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

        public Module SelectedModuleLocal 
        {
            get 
            {
                return selectedModuleLocal;
            }
            set 
            {
                selectedModuleLocal = value;
                RaisePropertyChanged(nameof(SelectedModuleLocal));
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

        public ModuleDetailViewModel()
        {
            //Messenger.Default.Register<Module>(this, OnModuleReceived);
            //Messenger.Default.Register<EditModuleMessage>(this, OnEditModuleMessageReceived);

            moduleDataService = new ModuleDataService();
            preRequisDataService = new PreRequisDataService();

            LoadModules();

            LoadCommands();

            MyMessenger<EditModuleMessage>.Instance.Register(OnEditModuleMessageReceived);           
        }        

        #endregion

        #region Les méthodes        

        private void LoadModules()
        {
            Modules = moduleDataService.GetAllElements().ToObservableCollection(); 
        }

        private void LoadCommands()
        {
            SaveModuleCommand = new RelayCommand(SaveModule, CanSaveModule);
            DeleteModuleCommand = new RelayCommand(DeleteModule, CanDeleteModule);
            UploadFileCommand = new RelayCommand(UploadFile, CanUploadFile);
            AddPreRequisCommand = new RelayCommand(AddPrerequis, CanAddPreRequis);
            DeletePreRequisCommand = new RelayCommand(DeletePreRequis, CanDeletePrerequis);
        }
        
        private void DeletePreRequis(object prerequis)
        {
            if(selectedModule.PreRequis != null)
            {
                selectedModule.PreRequis.Remove(selectedPreRequis);
            }
            
            ModulePreRequisites = selectedModule.PreRequis.ToObservableCollection();           
        }

        private bool CanDeletePrerequis(object obj)
        {
            if (selectedPreRequis != null)
                return true;
            return false;
        }

        private void AddPrerequis(object obj)
        {
            if( selectedModule.IdModule != selectedModuleLocal.IdModule)
            {
                SelectedPreRequis = new PreRequis
                {
                    IdModule = SelectedModule.IdModule,
                    Module = SelectedModule,
                    IdModulePreRequis = SelectedModuleLocal.IdModule,
                    ModulePreRequis = SelectedModuleLocal
                };

                //Ajouter le PreRequis dans la liste des Prérequis du Module sélectionné
                if (selectedModule.PreRequis == null)
                {                    
                    selectedModule.PreRequis = new List<PreRequis>(); ;
                }

                selectedModule.PreRequis.Add(selectedPreRequis); //les prerequis sont aussi ajoutés dans la liste des prérequis du module                                                                 
                ModulePreRequisites = selectedModule.PreRequis.ToObservableCollection();
            }            
        }

        private bool CanAddPreRequis(object obj)
        {
            if(SelectedModuleLocal != null)
                return true;
            return false;
        }

        private void UploadFile(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Sélectionner la table des matières au format PDF...",
                Filter = "PDF files (*.PDF)|*.PDF",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            Nullable<bool> result = openFileDialog.ShowDialog();

            if(result == true)
            {
                FileName = openFileDialog.FileName;

                uploadedFile = File.ReadAllBytes(fileName);
            }
        }

        private bool CanUploadFile(object obj)
        {
            return true;
        }

        private void OnEditModuleMessageReceived(EditModuleMessage obj)
        {
            SelectedModule = obj.SelectedModule;

            //Chercher tous les prérequis d'un module
            selectedModule.PreRequis = preRequisDataService.GetPreRequisOfModule(selectedModule.IdModule);

            if(selectedModule.PreRequis != null)
            {
                foreach(var pr in selectedModule.PreRequis)
                {
                    pr.ModulePreRequis = moduleDataService.GetElementDetail(pr.IdModulePreRequis);
                    pr.Module = moduleDataService.GetElementDetail(pr.IdModule);
                }
            }
            
            ModulePreRequisites = selectedModule.PreRequis.ToObservableCollection();
            LoadModules();
        }

        private void SaveModule(object obj)
        {
            if(uploadedFile != null)
            {
                selectedModule.TableDeMatieres = uploadedFile;
            }
            else
            {
                selectedModule.TableDeMatieres = (byte[])SelectedModule.TableDeMatieres;
            }            

            bool result = moduleDataService.UpdateElement(selectedModule);

            if (result)
            {
                if(selectedModule.PreRequis != null)
                {
                    foreach (var p in selectedModule.PreRequis)
                    {
                        //Tester si le prérequis existe déjà
                        PreRequis pr = preRequisDataService.GetElementDetail(p.IdModulePreRequis, p.IdModule);

                        if (pr != null)
                        {
                            preRequisDataService.UpdateElement(p);
                        }
                        else
                        {
                            preRequisDataService.CreateElement(p);
                        }                      
                    }
                }
                
                MyMessenger<ShowModuleViewMessage>.Instance.Send(new ShowModuleViewMessage());
                MyMessenger<UpdateModulesListMessage>.Instance.Send(new UpdateModulesListMessage());
            }
        }

        private bool CanSaveModule(object obj)
        {
            return true;
        }

        private void DeleteModule(object obj)
        {
            bool result1 = false;

            //Supprimer d'abord les clés etrangères
            if (selectedModule.PreRequis != null)
            {
                foreach(var pr in selectedModule.PreRequis)
                {
                    result1 = preRequisDataService.DeleteElement(pr);
                }
            }

            //Supprimer le module
            bool result = moduleDataService.DeleteElement(selectedModule);

            if (result)
            {
                MyMessenger<ShowModuleViewMessage>.Instance.Send(new ShowModuleViewMessage());
                MyMessenger<UpdateModulesListMessage>.Instance.Send(new UpdateModulesListMessage());
            }
        }

        private bool CanDeleteModule(object obj)
        {
            return true;
        }

        #endregion
    }
}
