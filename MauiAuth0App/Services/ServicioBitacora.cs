using MauiAuth0App.Models;
using System.Text.Json;

namespace MauiAuth0App.Services
{
    public static class ServicioBitacora
    {
        public async static void AgregarRegistro(int idUsuario, int idInstituto, string tipoAccion, string tabla)
        {

            var registro = new Bitacora
            {
                IdUsuario = idUsuario,
                IdInstituto = idInstituto,
                TipoAccion = tipoAccion,
                Tabla = tabla,
                Fecha = DateTime.Now
            };


            var jsonBitacora = JsonSerializer.Serialize(registro);
            using var httpClient = new HttpClient();
            var apiUrl = "http://ambetest.somee.com/api/Bitacora";
            try
            {

                var content = new StringContent(jsonBitacora, System.Text.Encoding.UTF8, "application/json");


                var response = await httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Entrada de bitácora enviada exitosamente a la API.");
                }
                else
                {
                    Console.WriteLine($"Error al enviar la entrada de bitácora a la API: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar la entrada de bitácora a la API: {ex.Message}");
            }

        }
    }
}
