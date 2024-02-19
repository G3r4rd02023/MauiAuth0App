using System.Text.Json.Serialization;

namespace MauiAuth0App.Models
{
    public class RolesViewModel
    {
        [JsonPropertyName("idRol")]
        public int IdRol { get; set; }

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }
    }
}
