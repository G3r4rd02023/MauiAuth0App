using MauiAuth0App.Models;
using MauiAuth0App.ViewModels;
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
        var response = await httpClient.GetAsync("http://ambetest.somee.com/api/Usuarios");
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<Usuarios>>(responseBody);
    }
}