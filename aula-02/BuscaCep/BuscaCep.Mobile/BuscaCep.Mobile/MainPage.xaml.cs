using BuscaCep.Mobile.ViewModels;

using Xamarin.Forms;

namespace BuscaCep.Mobile;

public partial class MainPage : ContentPage
{
    BuscaCepViewModel ViewModel { get => ((BuscaCepViewModel)BindingContext); }

    public MainPage()
    {
        InitializeComponent();
    }

    public class ViaCepDto
    {
        public string? cep { get; set; }
        public string? logradouro { get; set; }
        public string? complemento { get; set; }
        public string? bairro { get; set; }
        public string? localidade { get; set; }
        public string? uf { get; set; }
        public string? ibge { get; set; }
        public string? gia { get; set; }
        public string? ddd { get; set; }
        public string? siafi { get; set; }
        public bool? erro { get; set; }
    }
}
