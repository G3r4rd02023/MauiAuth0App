﻿using System.Text.Json.Serialization;

namespace MauiAuth0App.Models
{
    public class Usuarios
    {
        [JsonPropertyName("idUsuario")]
        public int IdUsuario { get; set; }

        [JsonPropertyName("idPersona")]
        public int? IdPersona { get; set; }

        [JsonPropertyName("usuario")]
        public string? Usuario { get; set; }

        [JsonPropertyName("idInstituto")]
        public int? IdInstituto { get; set; }

        [JsonPropertyName("nombreUsuario")]
        public string? NombreUsuario { get; set; }

        [JsonPropertyName("correoElectronico")]
        public string? CorreoElectronico { get; set; }

        [JsonPropertyName("estado")]
        public string? Estado { get; set; }

        [JsonPropertyName("idRol")]
        public int? IdRol { get; set; }

        [JsonPropertyName("fechaUltimaConexion")]
        public DateTime? FechaUltimaConexion { get; set; }

        
    }
}