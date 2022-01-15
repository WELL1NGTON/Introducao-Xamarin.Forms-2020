using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using BuscaCep.Mobile.Data;
using BuscaCep.Mobile.Data.Dtos;
using BuscaCep.Mobile.Services.Navigation;

using Newtonsoft.Json;

using Xamarin.Forms;

namespace BuscaCep.Mobile.ViewModels;

internal sealed class CepsViewModel : BasePageViewModel
{
    private string _CEP = string.Empty;

    private Command? _BuscarCommand;
    private Command? _RefreshCommand;
    private Command? _SelecionarCommand;

    public CepsViewModel()
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

    public ObservableCollection<CepDto> Ceps { get; private set; } = new();

    public Command BuscarCommand
        => _BuscarCommand ??= new Command(
            execute: async () => await BuscarCommandExecute(),
            canExecute: () => BuscarCommandCanExecute());

    public Command RefreshCommand
        => _RefreshCommand ??= new Command<bool>(
            execute: async (args) => await RefreshCommandExecute(args),
            canExecute: (args) => RefreshCommandCanExecute());

    public Command SelecionarCommand
        => _SelecionarCommand ??= new Command<object>(
            execute: async (args) => await SelecionarCommandExecute(args));

    internal override Task InitializeAsync(object? parameter)
        => Task.Factory.StartNew(() => RefreshCommand.Execute(true));

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

            if (MobileDatabaseService.Current.Get<CepDto>(lbda => lbda.cep.Equals(Regex.Replace(CEP, @"[^\d]", string.Empty))).Any())
            {
                await App.Current.MainPage.DisplayAlert("Oops", "O CEP já foi pesquisado...", "Ok");
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

            var cepDto = JsonConvert.DeserializeObject<CepDto>(content);

            if (cepDto is null || cepDto.erro is true)
            {
                throw new InvalidOperationException();
            }

            MobileDatabaseService.Current.Save(cepDto);

            RefreshCommand.Execute(true);
        }
        catch
        {
            await App.Current.MainPage.DisplayAlert("Oops", "Algo de errado não deu certo!", "Ok");
        }
        finally
        {
            IsBusy = false;

            BuscarCommand.ChangeCanExecute();
        }
    }

    private bool RefreshCommandCanExecute()
        => IsNotBusy;

    private async Task RefreshCommandExecute(bool force = false)
    {
        try
        {
            if (!force && IsBusy)
            {
                return;
            }

            IsBusy = true;
            BuscarCommand.ChangeCanExecute();
            RefreshCommand.ChangeCanExecute();

            Ceps.Clear();

            await Task.Factory.StartNew(() =>
            {
                foreach (var cep in MobileDatabaseService.Current.Get<CepDto>())
                {
                    Ceps.Add(cep);
                }
            });
        }
        catch
        {
            await App.Current.MainPage.DisplayAlert("Oops", "Algo de errado não deu certo!", "Ok");
        }
        finally
        {
            IsBusy = false;

            BuscarCommand.ChangeCanExecute();
        }
    }

    private Task SelecionarCommandExecute(object dto)
        => NavigationService.Current.Navigate<CepViewModel>((CepDto)dto);
}

