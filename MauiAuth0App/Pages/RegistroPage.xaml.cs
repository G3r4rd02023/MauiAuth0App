using MauiAuth0App.Models;
using MauiAuth0App.Services;
using System.Text;

namespace MauiAuth0App.Pages;

public partial class RegistroPage : ContentPage
{
    private readonly IServicioRoles _servicioRoles;
    private readonly IServicioInstituto _servicioInstituto;
    private readonly IServicioTipoPersona _servicioTipoPersona;
    private readonly string _usuario;

    public RegistroPage(IServicioRoles servicioRoles, IServicioInstituto servicioInstituto, IServicioTipoPersona servicioTipoPersona,
        string usuario)
    {
        InitializeComponent();
        _servicioRoles = servicioRoles;
        _servicioInstituto = servicioInstituto;
        _servicioTipoPersona = servicioTipoPersona;
        _usuario = usuario;
        CargarRoles();
        CargarInstitutos();
        CargarTipoPersonas();

    }

    private async void CargarRoles()
    {
        try
        {
            // Obtener la lista de roles desde el servicio
            var roles = await _servicioRoles.ObtenerLista();

            // Asignar la lista de roles como la fuente de datos del Picker
            pickerRol.ItemsSource = roles.Select(r => r.Descripcion).ToList();
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción que pueda ocurrir al obtener los roles
            await DisplayAlert("Error", "Hubo un problema al cargar los roles: " + ex.Message, "OK");
        }
    }

    private async void CargarInstitutos()
    {
        try
        {
            var institutos = await _servicioInstituto.ObtenerLista();
            pickerInstituto.ItemsSource = institutos.Select(r => r.NombreInstituto).ToList();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Hubo un problema al cargar los institutos: " + ex.Message, "OK");
        }
    }

    private async void CargarTipoPersonas()
    {
        try
        {
            var tipoPersonas = await _servicioTipoPersona.ObtenerLista();
            pickerTipoPersona.ItemsSource = tipoPersonas.Select(r => r.TipoPersona).ToList();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Hubo un problema al cargar los institutos: " + ex.Message, "OK");
        }
    }

    private async void RegistrarUsuario(object sender, EventArgs e)
    {
       
        // Obtener los valores ingresados por el usuario
        string primerNombre = TxtPrimerNombre.Text;
        string segundoNombre = TxtSegundoNombre.Text;
        string primerApellido = TxtPrimerApellido.Text;
        string segundoApellido = TxtSegundoApellido.Text;
        DateTime fechaNacimiento = DpFechaNacimiento.Date;
        string genero = ChkMasculino.IsChecked ? "Masculino" : "Femenino";
        int idRol = await _servicioRoles.ObtenerIdRolPorNombre(pickerRol.SelectedItem.ToString());
        int idInstituto = await _servicioInstituto.ObtenerIdInstitutoPorNombre(pickerInstituto.SelectedItem.ToString());
        int idTipoPersona = await _servicioTipoPersona.ObtenerIdTipoPersonaPorNombre(pickerTipoPersona.SelectedItem.ToString());
        string usuario = _usuario;




        // Validar que los campos requeridos no estén vacíos
        if (string.IsNullOrWhiteSpace(primerNombre) || string.IsNullOrWhiteSpace(primerApellido))
        {
            await DisplayAlert("Error", "Los campos Primer Nombre y Primer Apellido son obligatorios", "OK");
            return;
        }

        // Crear un objeto para representar al usuario
        var persona = new PersonaViewModel
        {
            PrimerNombre = primerNombre,
            SegundoNombre = segundoNombre,
            PrimerApellido = primerApellido,
            SegundoApellido = segundoApellido,
            FechaNacimiento = fechaNacimiento,
            Genero = genero,
            IdRol = idRol,
            IdInstituto = idInstituto,
            IdTipoPersona = idTipoPersona,
            Usuario = usuario,
            NombreUsuario = usuario,
            CorreoElectronico = usuario,
            Contraseña = "hsdfgjhgfjbfxsl",
            Estado = "Nuevo",
            FechaUltimaConexion = DateTime.Now,
            CreadoPor = usuario,
            ModificadoPor = usuario
        };

        // Convertir el objeto usuario a formato JSON
        string personaJson = System.Text.Json.JsonSerializer.Serialize(persona);

        // Enviar los datos del usuario a través de una API
        var httpClient = new HttpClient();
        var content = new StringContent(personaJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await httpClient.PostAsync("http://ambetest.somee.com/api/Usuarios", content);

        // Verificar si la solicitud fue exitosa
        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Éxito", "Usuario registrado correctamente", "OK");

            int idUsuario = await _servicioRoles.ObtenerIdUsuario(_usuario);

            ServicioBitacora.AgregarRegistro(idUsuario,idInstituto,"Registro","Usuario");
            
        }
        else
        {
            await DisplayAlert("Error", "Hubo un problema al registrar el usuario. Por favor, intenta nuevamente.", "OK");
        }
    }
}