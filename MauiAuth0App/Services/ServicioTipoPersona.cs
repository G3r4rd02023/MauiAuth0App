using MauiAuth0App.Models;
using System.Text.Json;

namespace MauiAuth0App.Services
{
    public class ServicioTipoPersona : IServicioTipoPersona
    {
        private string urlApi = "http://ambetest.somee.com/api/TipoPersonas";
        public async Task<int> ObtenerIdTipoPersonaPorNombre(string tipoPersona)
        {
            try
            {
                var tipoPersonas = await ObtenerLista();

                var tipo = tipoPersonas.FirstOrDefault(r => r.TipoPersona == tipoPersona);


                return tipo != null ? tipo.IdTipoPersona : -1;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al obtener los roles: {ex.Message}");
                return -1;
            }
        }

        public async Task<List<TipoPersonasViewModel>> ObtenerLista()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var tipoPersonasData = JsonSerializer.Deserialize<List<TipoPersonasViewModel>>(responseBody);
                return tipoPersonasData;
            }
            else
            {                
                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                return new List<TipoPersonasViewModel>();
            }
        }
    }
}
