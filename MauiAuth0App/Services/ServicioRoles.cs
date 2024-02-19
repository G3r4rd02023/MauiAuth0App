using MauiAuth0App.Models;
using System.Text.Json;

namespace MauiAuth0App.Services
{
    public class ServicioRoles:IServicioRoles
    {
        private string urlApi = "http://ambetest.somee.com/api/Roles";

       
        public async Task<List<RolesViewModel>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var rolesData = JsonSerializer.Deserialize<List<RolesViewModel>>(responseBody);
                return rolesData;
            }
            else
            {
                // Manejar el caso cuando la solicitud no es exitosa.
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return new List<RolesViewModel>();
            }
        }

        public async Task<int> ObtenerIdRolPorNombre(string nombreRol)
        {
            try
            {
                // Obtener la lista de roles desde la API
                var roles = await ObtenerLista();

                // Buscar el rol por nombre en la lista de roles
                var rol = roles.FirstOrDefault(r => r.Descripcion == nombreRol);

                // Devolver el ID del rol encontrado, o -1 si no se encuentra
                return rol != null ? rol.IdRol : -1;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir al obtener los roles
                Console.WriteLine($"Error al obtener los roles: {ex.Message}");
                return -1;
            }
        }


    }
}
