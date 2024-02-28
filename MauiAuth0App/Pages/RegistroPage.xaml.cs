using MauiAuth0App.Auth0;
using MauiAuth0App.Models;
using MauiAuth0App.Services;
using System.Text;

namespace MauiAuth0App.Pages;

public partial class RegistroPage : ContentPage
{


    private readonly Auth0Client auth0Client;
    private readonly string _usuario;

    public RegistroPage(string usuario,Auth0Client client)
    {
        InitializeComponent();                       
        _usuario = usuario;
        auth0Client = client;
        CargarRoles();
        CargarInstitutos();
        CargarTipoPersonas();

    }

    private async void CargarRoles()
    {
        try
        {
            // Obtener la lista de roles desde el servicio
            ServicioRoles servicioRoles = new();
            var roles = await servicioRoles.ObtenerLista();
            


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
            ServicioInstituto servicioInstituto = new();
            List<InstitutosViewModel> lista = await servicioInstituto.ObtenerLista();
            
            pickerInstituto.ItemsSource = lista.Select(r => r.NombreInstituto).ToList();
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
            ServicioTipoPersona servicioTipoPersona = new();
            var tipoPersonas = await servicioTipoPersona.ObtenerLista();
            pickerTipoPersona.ItemsSource = tipoPersonas.Select(r => r.TipoPersona).ToList();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Hubo un problema al cargar los institutos: " + ex.Message, "OK");
        }
    }

    private async void RegistrarUsuario(object sender, EventArgs e)
    {

        try
        {
            if (pickerRol.SelectedItem == null || string.IsNullOrEmpty(pickerRol.SelectedItem.ToString()))
            {
                await DisplayAlert("Error", "Por favor, selecciona un rol.", "OK");
            }

            if (pickerInstituto.SelectedItem == null || string.IsNullOrEmpty(pickerInstituto.SelectedItem.ToString()))
            {
                await DisplayAlert("Error", "Por favor, selecciona un instituto.", "OK");
            }

            if (pickerTipoPersona.SelectedItem == null || string.IsNullOrEmpty(pickerTipoPersona.SelectedItem.ToString()))
            {
                await DisplayAlert("Error", "Por favor, selecciona un tipo de persona.", "OK");
            }

            // Obtener los valores ingresados por el usuario
            string primerNombre = TxtPrimerNombre.Text;
            string segundoNombre = TxtSegundoNombre.Text;
            string primerApellido = TxtPrimerApellido.Text;
            string segundoApellido = TxtSegundoApellido.Text;
            string correoElectronico = TxtCorreo.Text;
            DateTime fechaNacimiento = DpFechaNacimiento.Date;
            string genero = ChkMasculino.IsChecked ? "Masculino" : "Femenino";
            ServicioRoles servicioRoles = new();
            int idRol = await servicioRoles.ObtenerIdRolPorNombre(pickerRol.SelectedItem.ToString());
            ServicioInstituto servicioInstituto = new();
            int idInstituto = await servicioInstituto.ObtenerIdInstitutoPorNombre(pickerInstituto.SelectedItem.ToString());
            ServicioTipoPersona servicioTipoPersona = new();
            int idTipoPersona = await servicioTipoPersona.ObtenerIdTipoPersonaPorNombre(pickerTipoPersona.SelectedItem.ToString());
            string usuario = _usuario;

            //validar fecha nacimiento
            if (fechaNacimiento > DateTime.Today)
            {
                await DisplayAlert("Error", "La fecha no puede ser posterior a la fecha actual", "OK");
                return;
            }


            // Validar que los campos requeridos no estén vacíos
            if (string.IsNullOrWhiteSpace(primerNombre) || string.IsNullOrWhiteSpace(primerApellido))
            {
                await DisplayAlert("Error", "Los campos Primer Nombre y Primer Apellido son obligatorios", "OK");
                return;
            }

            //validar correo
            if (string.IsNullOrWhiteSpace(correoElectronico))
            {
                await DisplayAlert("Error", "El campo correo electronico es obligatorio", "OK");
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

            
            bool usuarioExiste = await ServicioRoles.UsuarioExiste(_usuario);
            if (usuarioExiste)
            {
                await DisplayAlert("Error", "El usuario ya está registrado.", "OK");
            }
            else
            {
                // Enviar los datos del usuario a través de una API
                var httpClient = new HttpClient();
                var content = new StringContent(personaJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("https://ambetest.somee.com/api/Usuarios", content);

                // Verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {

                    await DisplayAlert("Éxito", "Usuario registrado correctamente, tu perfil necesita ser aprobado por el administrador"
                        , "OK");

                    int idUsuario = await ServicioRoles.ObtenerIdUsuario(_usuario);

                    ServicioBitacora.AgregarRegistro(idUsuario, idInstituto, "Registro", "Usuario");
                    
                    await Navigation.PushAsync(new MainPage(auth0Client));

                }
                else
                {
                    await DisplayAlert("Error", "Hubo un problema al registrar el usuario. Por favor, intenta nuevamente.", "OK");
                }

            }
        }
        catch (Exception ex)
        {

            await DisplayAlert("Error", $"Por favor complete todos los campos : {ex.Message}", "OK");
            return;
        }        
        
    }
}