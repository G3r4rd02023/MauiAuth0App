using MauiAuth0App.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiAuth0App.ViewModels
{
    public class UsuariosViewModel : INotifyPropertyChanged
    {
        private List<Usuarios> _usuarios;

        public List<Usuarios> Usuarios
        {
            get => _usuarios;
            set
            {
                _usuarios = value;
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
