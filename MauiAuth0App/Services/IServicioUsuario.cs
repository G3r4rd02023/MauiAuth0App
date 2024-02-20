namespace MauiAuth0App.Services
{
    public interface IServicioUsuario
    {
        Task<bool> ValidarPrimerLogin(string usuario);

    }
}
