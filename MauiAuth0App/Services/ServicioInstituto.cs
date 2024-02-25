using MauiAuth0App.Models;
using System.Text.Json;

namespace MauiAuth0App.Services
{
    public class ServicioInstituto : IServicioInstituto
    {
        private string urlApi = "https://ambetest.somee.com/api/Institutos";
        public async Task<int> ObtenerIdInstitutoPorNombre(string nombreInstituto)
        {
            try
            {

                var institutos = await ObtenerLista();


                var instituto = institutos.FirstOrDefault(r => r.NombreInstituto == nombreInstituto);


                return instituto != null ? instituto.IdInstituto : -1;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al obtener los roles: {ex.Message}");
                return -1;
            }
        }

        public async Task<List<InstitutosViewModel>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var institutoData = JsonSerializer.Deserialize<List<InstitutosViewModel>>(responseBody);
                return institutoData;
            }
            else
            {
                // Manejar el caso cuando la solicitud no es exitosa.
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return new List<InstitutosViewModel>();
            }
        }
    }
}
