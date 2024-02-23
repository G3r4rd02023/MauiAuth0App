using MauiAuth0App.Models;

namespace MauiAuth0App.Services
{
    public interface IServicioRoles
    {
        public Task<List<RolesViewModel>> ObtenerLista();

        Task<int> ObtenerIdRolPorNombre(string nombreRol);

        Task<int> ObtenerIdUsuario(string usuario);

        Task<bool> UsuarioExiste(string usuario);

    }
}
