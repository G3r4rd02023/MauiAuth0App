using MauiAuth0App.Models;

namespace MauiAuth0App.Services
{
    public interface IServicioInstituto
    {
        public Task<List<InstitutosViewModel>> ObtenerLista();

        Task<int> ObtenerIdInstitutoPorNombre(string nombreInstituto);

    }
}
