namespace MauiAuth0App.Models
{
    public class PersonaViewModel
    {
        public int IdPersona { get; set; }

        public int IdTipoPersona { get; set; }

        public int IdInstituto { get; set; }

        public string? PrimerNombre { get; set; }

        public string? SegundoNombre { get; set; }

        public string? PrimerApellido { get; set; }

        public string? SegundoApellido { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public string? Genero { get; set; }

        public string? Usuario { get; set; }

        public string? NombreUsuario { get; set; }

        public string? CorreoElectronico { get; set; }

        public string? Contraseña { get; set; }

        public string? Estado { get; set; }

        public int IdRol { get; set; }

        public DateTime? FechaUltimaConexion { get; set; }

        public string? CreadoPor { get; set; }

        public string? ModificadoPor { get; set; }
    }
}
