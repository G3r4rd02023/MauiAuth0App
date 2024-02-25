using MauiAuth0App.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace MauiAuth0App.ViewModels
{
    public class RolesViewModel : INotifyPropertyChanged
    {
        private List<Roles> _roles;

        public List<Roles> Roles
        {
            get => _roles;
            set
            {
                _roles = value;
                OnPropertyChanged(); // Asegúrate de que la vista se actualice cuando se cambie la lista de usuarios
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
