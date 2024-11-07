using HistoricoTempo.Models;
using HistoricoTempo.Service;
using System.Diagnostics;

namespace HistoricoTempo
{
    public partial class MainPage : ContentPage
    {
        CancellationTokenSource _cancelTokenSource;
        bool _isCheckingLocation;

        string? cidade;

        public MainPage()
        {
            InitializeComponent();
        }

        // #D8D8FA

        private void OnCounterClicked(object sender, EventArgs e)
        {
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                _cancelTokenSource = new CancellationTokenSource();

                GeolocationRequest request =
                    new GeolocationRequest(GeolocationAccuracy.Medium,
                    TimeSpan.FromSeconds(10));

                Location? location = await Geolocation.Default.GetLocationAsync(
                    request, _cancelTokenSource.Token);

                if (location != null)
                {
                    lbl_latitude.Text = location.Latitude.ToString();
                    lbl_longitude.Text = location.Longitude.ToString();

                    Debug.WriteLine("-------------------------------------------");
                    Debug.WriteLine(location);
                    Debug.WriteLine("-------------------------------------------");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Erro: dispositivo não suporta",
                    fnsEx.Message, "OK");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                await DisplayAlert("Erro: localização desabilitada",
                    fneEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Erro: permissão", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro:", ex.Message, "OK");
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(cidade))
                {
                    Tempo? previsao = await DataService.GetPrevisaoDoTempo(cidade);

                    string dados_previsao = "";
                }
            }
            finally
            {
            }
        }
    }

}
