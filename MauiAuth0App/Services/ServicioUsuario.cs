using MauiAuth0App.Models;
using System.Text.Json;

namespace MauiAuth0App.Services
{
    public class ServicioUsuario : IServicioUsuario
    {
        private string urlApi = "http://ambetest.somee.com/api/Usuarios";

        public async Task<List<UsuariosViewModel>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var usuariosData = JsonSerializer.Deserialize<List<UsuariosViewModel>>(responseBody);
                return usuariosData;
            }
            else
            {
                // Manejar el caso cuando la solicitud no es exitosa.
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return new List<UsuariosViewModel>();
            }
        }
        public async Task<bool> ValidarPrimerLogin(string usuario)
        {
            var listaUsuarios = await ObtenerLista();

            var usuarioEncontrado = listaUsuarios.FirstOrDefault(u => u.NombreUsuario == usuario || u.Usuario == usuario);

            if (usuarioEncontrado != null && usuarioEncontrado.Estado == "Nuevo")
            {                
                //accede directamente a menu
                return false; 
            }
            else
            {
                //envia a registro
                return true; 
            }
        }        
    }
}
