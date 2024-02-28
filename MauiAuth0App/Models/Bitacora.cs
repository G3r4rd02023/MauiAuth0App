using System.Text.Json.Serialization;

namespace MauiAuth0App.Models
{
    public class Bitacora
    {
        [JsonPropertyName("idUsuario")]
        public int IdUsuario { get; set; }

        [JsonPropertyName("idInstituto")]
        public int? IdInstituto { get; set; }

        [JsonPropertyName("tipoAccion")]
        public string? TipoAccion { get; set; }

        [JsonPropertyName("tabla")]
        public string? Tabla { get; set; }

        [JsonPropertyName("fecha")]
        public DateTime? Fecha { get; set; }
    }
}
