using MauiAuth0App.Models;
using MauiAuth0App.ViewModels;
using System.Text;
using System.Text.Json;

namespace MauiAuth0App.Pages;

public partial class UsuariosPage : ContentPage
{

    private readonly UsuariosViewModel _viewModel;


    public UsuariosPage()
    {
        InitializeComponent();
        _viewModel = new UsuariosViewModel();
        BindingContext = _viewModel;
        CargarUsuarios();
    }

    private async void CargarUsuarios()
    {
        var usuarios = await ObtenerUsuarios();
        _viewModel.Usuarios = usuarios;
    }

    private async Task<List<Usuarios>> ObtenerUsuarios()
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync("https://ambetest.somee.com/api/Usuarios");
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<Usuarios>>(responseBody);
    }

    private async void EditarUsuario_Clicked(object sender, EventArgs e)
    {
        var boton = (Button)sender;
        var usuario = boton.BindingContext as Usuarios;

        if (usuario != null)
        {
            // Navegar a la página de edición de usuario y pasar el usuario como parámetro
            await Navigation.PushAsync(new EditarUsuarioPage(usuario));
        }
        else
        {
            await DisplayAlert("Error", "No se pudo obtener el usuario para editar", "OK");
        }
    }

}