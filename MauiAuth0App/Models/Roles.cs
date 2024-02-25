using System.Text.Json.Serialization;

namespace MauiAuth0App.Models
{
    public class Roles
    {
        [JsonPropertyName("idRol")]
        public int IdRol { get; set; }

        [JsonPropertyName("idInstituto")]
        public int? IdInstituto { get; set; }

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }

        [JsonPropertyName("creadoPor")]
        public string? CreadoPor { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("mmodificadoPor")]
        public string? ModificadoPor { get; set; }

        [JsonPropertyName("fechaModificacion")]
        public DateTime? FechaModificacion { get; set; }
    }
}
