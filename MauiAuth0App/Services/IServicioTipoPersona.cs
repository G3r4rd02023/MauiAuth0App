using MauiAuth0App.Models;

namespace MauiAuth0App.Services
{
    public interface IServicioTipoPersona
    {
        public Task<List<TipoPersonasViewModel>> ObtenerLista();

        Task<int> ObtenerIdTipoPersonaPorNombre(string tipoPersona);
    }
}

