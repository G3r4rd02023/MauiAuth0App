using MauiAuth0App.Models;

namespace MauiAuth0App.Services
{
    public interface IServicioUsuario
    {
        Task<bool> ValidarPrimerLogin(string usuario);

        Task<List<Usuarios>> ObtenerLista();
    }
}
