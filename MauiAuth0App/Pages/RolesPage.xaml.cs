using MauiAuth0App.Models;
using MauiAuth0App.ViewModels;
using System.Text.Json;

namespace MauiAuth0App.Pages;

public partial class RolesPage : ContentPage
{

    private readonly RolesViewModel _viewModel;

    public RolesPage()
    {
        InitializeComponent();
        _viewModel = new RolesViewModel();
        BindingContext = _viewModel;
        CargarRoles();
    }

    private async void CargarRoles()
    {
        var registros = await ObtenerRoles();
        _viewModel.Roles = registros;
    }
    private async Task<List<Roles>> ObtenerRoles()
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync("https://ambetest.somee.com/api/Roles");
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<Roles>>(responseBody);
    }

    private async void CrearRol_Clicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new CrearRolPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error : {ex.Message}", "OK");
            return;
        }
    }
}