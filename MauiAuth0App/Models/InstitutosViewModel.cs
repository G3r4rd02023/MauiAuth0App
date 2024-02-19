using System.Text.Json.Serialization;

namespace MauiAuth0App.Models
{
    public class InstitutosViewModel
    {
        [JsonPropertyName("idInstituto")]
        public int IdInstituto { get; set; }

        [JsonPropertyName("nombreInstituto")]
        public string? NombreInstituto { get; set; }

    }
}
