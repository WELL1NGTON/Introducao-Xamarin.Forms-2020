using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using BuscaCep.Mobile.Data.Dtos;

namespace BuscaCep.Mobile.ViewModels;

internal class BuscaCepViewModel : BaseViewModel
{
    private string _CEP = string.Empty;

    private CepDto? _ViaCepDto = null;

    private Command? _BuscarCommand;

    public BuscaCepViewModel()
    {
    }

    public string CEP
    {
        get { return _CEP; }
        set
        {
            _CEP = value;
            OnPropertyChanged();
            BuscarCommand.ChangeCanExecute();
        }
    }

    public bool HasCep { get => _ViaCepDto is not null; }

    public string Logradouro
    {
        get { return _ViaCepDto?.logradouro ?? string.Empty; }
    }

    public string Complemento
    {
        get { return _ViaCepDto?.complemento ?? string.Empty; }
    }

    public string Bairro
    {
        get { return _ViaCepDto?.bairro ?? string.Empty; }
    }

    public string Localidade
    {
        get { return _ViaCepDto?.localidade ?? string.Empty; }
    }

    public string UF
    {
        get { return _ViaCepDto?.uf ?? string.Empty; }
    }

    public Command BuscarCommand
        => _BuscarCommand ??= new Command(
            execute: async () => await BuscarCommandExecute(),
            canExecute: () => BuscarCommandCanExecute());

    private bool BuscarCommandCanExecute()
        => !string.IsNullOrWhiteSpace(CEP)
        && CEP.Length == 8
        && IsNotBusy;

    private async Task BuscarCommandExecute()
    {
        try
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            BuscarCommand.ChangeCanExecute();

            using var client = new HttpClient();
            using var response = await client.GetAsync($"https://viacep.com.br/ws/{CEP}/json/");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new InvalidOperationException();
            }

            _ViaCepDto = JsonConvert.DeserializeObject<CepDto>(content);

            if (_ViaCepDto is null || (_ViaCepDto.erro ?? false))
            {
                throw new InvalidOperationException();
            }
        }
        catch
        {
            _ViaCepDto = null;

            await App.Current.MainPage.DisplayAlert("Oops", "Algo de errado não deu certo!", "Ok");
        }
        finally
        {
            OnPropertyChanged(nameof(HasCep));
            OnPropertyChanged(nameof(Logradouro));
            OnPropertyChanged(nameof(Complemento));
            OnPropertyChanged(nameof(Bairro));
            OnPropertyChanged(nameof(Localidade));
            OnPropertyChanged(nameof(UF));

            IsBusy = false;

            BuscarCommand.ChangeCanExecute();
        }
    }

}

