using MauiAuth0App.Models;
using MauiAuth0App.ViewModels;
using System.Text.Json;

namespace MauiAuth0App.Pages;

public partial class BitacoraPage : ContentPage
{
    private readonly BitacoraViewModel _viewModel;
    public BitacoraPage()
	{
		InitializeComponent();
        _viewModel = new BitacoraViewModel();
        BindingContext = _viewModel;
        CargarBitacora();
    }

    private async void CargarBitacora()
    {
        var registros = await ObtenerBitacora();
        _viewModel.Bitacoras = registros;
    }

    private async Task<List<Bitacora>> ObtenerBitacora()
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync("https://ambetest.somee.com/api/Bitacora");
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<Bitacora>>(responseBody);
    }
}