using MauiAuth0App.Models;
using System.Text.Json;

namespace MauiAuth0App.Services
{
    public class ServicioRoles
    {
        private readonly string urlApi = "https://ambetest.somee.com/api/Roles";
      
        public async Task<List<Roles>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var rolesData = JsonSerializer.Deserialize<List<Roles>>(responseBody);
                return rolesData;
            }
            else
            {
                // Manejar el caso cuando la solicitud no es exitosa.               
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return new List<Roles>();
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

       
        public static async Task<int> ObtenerIdUsuario(string usuario)
        {
            try
            {
                ServicioUsuario servicioUsuario = new();
                var usuarios = await servicioUsuario.ObtenerLista();
                var user = usuarios.FirstOrDefault(u => u.Usuario == usuario);
                return user != null ? user.IdUsuario : -1;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al obtener los usuarios: {ex.Message}");
                return -1;
            }
        }

        public static async Task<bool> UsuarioExiste(string usuario)
        {
            try
            {
                ServicioUsuario servicioUsuario = new();
                var usuarios = await servicioUsuario.ObtenerLista();
                var user = usuarios.FirstOrDefault(u => u.Usuario == usuario);

                if (user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los usuarios: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RolExiste(string nombreRol)
        {
            try
            {
                var roles = await ObtenerLista();
                var rolEncontrado = roles.FirstOrDefault(u => u.Descripcion == nombreRol);

                if (rolEncontrado != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los roles: {ex.Message}");
                return false;
            }
        }

        public async Task<string> ObtenerNombreRol(int idRol)
        {
            try
            {
                var roles = await ObtenerLista();
                var rolEncontrado = roles.FirstOrDefault(r => r.IdRol == idRol);
                return rolEncontrado != null ? rolEncontrado.Descripcion : "Rol no encontrado";
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Error al obtener el nombre del rol: {ex.Message}");
                return "Error";
            }
        }
    }
}
