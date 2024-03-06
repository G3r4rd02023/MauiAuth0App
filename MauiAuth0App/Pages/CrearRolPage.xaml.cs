using MauiAuth0App.Models;
using MauiAuth0App.Services;
using System.Text;

namespace MauiAuth0App.Pages;

public partial class CrearRolPage : ContentPage
{


    public CrearRolPage()
    {
        InitializeComponent();
        CargarInstitutos();
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

    private async void CrearRol(object sender, EventArgs e)
    {
        try
        {
            if (pickerInstituto.SelectedItem == null || string.IsNullOrEmpty(pickerInstituto.SelectedItem.ToString()))
            {
                await DisplayAlert("Error", "Por favor, selecciona un instituto.", "OK");
            }

            string descripcion = TxtDescripcion.Text;
            ServicioInstituto servicioInstituto = new();
            int idInstituto = await servicioInstituto.ObtenerIdInstitutoPorNombre(pickerInstituto.SelectedItem.ToString());
            var username = ServicioUsuario.UsuarioAutenticado;


            if (string.IsNullOrWhiteSpace(descripcion))
            {
                await DisplayAlert("Error", "El campo descripcion es obligatorio", "OK");
                return;
            }

            var rol = new Roles()
            {
                IdInstituto = idInstituto,
                Descripcion = descripcion,
                CreadoPor = username,
                FechaCreacion = DateTime.Now,
                ModificadoPor = username,
                FechaModificacion = DateTime.Now
            };

            string rolJson = System.Text.Json.JsonSerializer.Serialize(rol);


            ServicioRoles servicioRoles = new();
            bool rolExiste = await servicioRoles.RolExiste(descripcion);

            if (rolExiste)
            {
                await DisplayAlert("Error", "El rol ya está existe.", "OK");
            }
            else
            {
                // Enviar los datos del rol a través de una API
                var httpClient = new HttpClient();
                var content = new StringContent(rolJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("https://ambetest.somee.com/api/Roles", content);

                // Verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {

                    await DisplayAlert("Éxito", "Rol creado correctamente, pero aun necesita ser aprobado por el administrador"
                        , "OK");

                    int idUsuario = await ServicioRoles.ObtenerIdUsuario(username!);

                    ServicioBitacora.AgregarRegistro(idUsuario, idInstituto, "Creo", "Roles");

                    await Navigation.PopAsync();

                }
                else
                {
                    await DisplayAlert("Error", "Hubo un problema al crear el rol. Por favor, intenta nuevamente.", "OK");
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