using GestionDeNotreCentre.App.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
//using System.Windows.Input;

namespace GestionDeNotreCentre.App.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged //ValidationRule,
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelBase()
        {
            Type TViewModel = GetType();

            IEnumerable<PropertyInfo> commandes = TViewModel.GetProperties().Where(pi => pi.PropertyType == typeof(ICommand) || pi.PropertyType.GetInterfaces().Contains(typeof(ICommand)));

            foreach (PropertyInfo propertyInfo in commandes)
            {
                ICommand commande = (ICommand)propertyInfo.GetMethod.Invoke(this, null);
                PropertyChanged += (s, e) => commande.RaiseCanExecuteChanged();
            }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
