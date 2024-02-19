using System.Text.Json.Serialization;

namespace MauiAuth0App.Models
{
    public class TipoPersonasViewModel
    {
        [JsonPropertyName("idTipoPersona")]
        public int IdTipoPersona { get; set; }

        [JsonPropertyName("tipoPersona")]
        public string? TipoPersona { get; set; }
    }
}
