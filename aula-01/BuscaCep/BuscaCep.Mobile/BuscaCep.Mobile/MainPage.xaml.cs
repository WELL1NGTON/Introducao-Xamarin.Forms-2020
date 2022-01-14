using Newtonsoft.Json;
using System;
using System.Net.Http;
using Xamarin.Forms;

namespace BuscaCep.Mobile;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtCep.Text))
            {
                return;
            }

            using var client = new HttpClient();
            using var response = await client.GetAsync($"https://viacep.com.br/ws/{txtCep.Text}/json/");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new InvalidOperationException();
            }

            var retorno = JsonConvert.DeserializeObject<ViaCepDto>(content);

            if (retorno is null || (retorno.erro ?? false))
            {
                throw new InvalidOperationException();
            }

            txtLogradouro.Text = retorno.logradouro;
            txtComplemento.Text = retorno.complemento;
            txtBairro.Text = retorno.bairro;
            txtLocalidade.Text = retorno.localidade;
            txtUF.Text = retorno.uf;
        }
        catch (Exception)
        {
            await DisplayAlert("Oops", "Algo de errado não deu certo!", "Ok");
        }
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
