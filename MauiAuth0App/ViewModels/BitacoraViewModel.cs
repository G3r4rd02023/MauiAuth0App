using MauiAuth0App.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiAuth0App.ViewModels
{
    public class BitacoraViewModel : INotifyPropertyChanged
    {
        private List<Bitacora> _bitacora;

        public List<Bitacora> Bitacoras
        {
            get => _bitacora;
            set
            {
                _bitacora = value;
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
