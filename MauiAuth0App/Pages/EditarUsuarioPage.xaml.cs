using MauiAuth0App.Models;
using MauiAuth0App.Services;
using System.Text;

namespace MauiAuth0App.Pages;

public partial class EditarUsuarioPage : ContentPage
{
    public Usuarios Usuario { get; set; }
   
    public EditarUsuarioPage(Usuarios usuario)
    {
        InitializeComponent();
        Usuario = usuario;
        pickerEstado.Items.Add("Nuevo");
        pickerEstado.Items.Add("Activo");
        pickerEstado.Items.Add("Bloqueado");
        pickerEstado.Items.Add("Inactivo");
        pickerEstado.SelectedIndex = 0;
        CargarRoles();
        TxtUsuario.Text = usuario.Usuario ?? string.Empty;
        TxtNombreUsuario.Text = usuario.NombreUsuario ?? string.Empty;
        TxtCorreo.Text = usuario.CorreoElectronico ?? string.Empty;
        TxtFechaUltimaConexion.Text = usuario.FechaUltimaConexion?.ToString("dd/MM/yyyy") ?? string.Empty;
    }

    private async void CargarRoles()
    {
        try
        {           
            ServicioRoles servicioRoles = new();
            var roles = await servicioRoles.ObtenerLista();           
            pickerRol.ItemsSource = roles.Select(r => r.Descripcion).ToList();
        }
        catch (Exception ex)
        {            
            await DisplayAlert("Error", "Hubo un problema al cargar los roles: " + ex.Message, "OK");
        }
    }
    private async void EditarUsuario(object sender, EventArgs e)
    {
        try
        {
            // Aquí puedes implementar la lógica para validar los campos y obtener los nuevos valores del usuario a editar
            if (pickerRol.SelectedItem == null || string.IsNullOrEmpty(pickerRol.SelectedItem.ToString()))
            {
                await DisplayAlert("Error", "Por favor, selecciona un rol.", "OK");
            }



           


            // Obtener los valores ingresados o modificados por el usuario
            int idUsuario = Usuario.IdUsuario;
            int? idPersona = Usuario.IdPersona;
            string? usuario = Usuario.Usuario;
            int? idInstituto = Usuario.IdInstituto;
            string? nombreUsuario = Usuario.NombreUsuario;            
            string? correoElectronico = Usuario.CorreoElectronico;
            string? Estado = Usuario.Estado;
            int? idRol = Usuario.IdRol;
            DateTime? fechaUltimaConexion = Usuario.FechaUltimaConexion;
            
            
            // Aquí deberías obtener los IDs correspondientes a los nuevos valores de rol, instituto y tipo de persona
            ServicioRoles servicioRoles = new();
            int nuevoIdRol = await servicioRoles.ObtenerIdRolPorNombre(pickerRol.SelectedItem.ToString());

            string nuevoEstado = pickerEstado.SelectedItem.ToString();

            // Supongamos que ya tienes esta lógica implementada

            // Validar los campos necesarios


            // Validar la fecha de nacimiento


            // Aquí deberías realizar la lógica para obtener el usuario que se va a editar, por ejemplo, mediante una consulta a la base de datos o a través de una llamada a una API
            // Supongamos que ya tienes esta lógica implementada y que has obtenido el objeto 'persona' que representa al usuario que se va a editar
            Usuarios user = new()
            {
                // Actualizar los datos del usuario con los nuevos valores
                IdUsuario = idUsuario,
                IdPersona = idPersona,
                Usuario = usuario,
                IdInstituto = idInstituto,
                NombreUsuario = nombreUsuario,
                Contraseña = "dg2do7xbxjksjs",
                CorreoElectronico = correoElectronico,
                Estado = nuevoEstado,
                IdRol = nuevoIdRol,
                FechaUltimaConexion = fechaUltimaConexion,
                CreadoPor = Usuario.CreadoPor,
                FechaCreacion = Usuario.FechaCreacion,
                ModificadoPor = "ADMINISTRADOR",
                FechaModificacion = DateTime.Now
            };
            

            // Convertir el objeto persona a formato JSON
            string userJson = System.Text.Json.JsonSerializer.Serialize(user);

            // Enviar los datos del usuario actualizado a través de una API (usando el método PUT para actualizar)
            var httpClient = new HttpClient();
            var content = new StringContent(userJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PutAsync("https://ambetest.somee.com/api/Usuarios/" + user.IdUsuario, content);

            // Verificar si la solicitud fue exitosa
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Usuario actualizado correctamente", "OK");

                // Aquí puedes manejar la navegación de regreso o a otra página si es necesario
                await Navigation.PopAsync(); // Por ejemplo, regresar a la página anterior
            }
            else
            {
                await DisplayAlert("Error", "Hubo un problema al actualizar el usuario. Por favor, intenta nuevamente.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ha ocurrido un error: {ex.Message}", "OK");
        }
    }

}