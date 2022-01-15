using System;
using System.Threading.Tasks;

using BuscaCep.Mobile.Data.Dtos;

namespace BuscaCep.Mobile.ViewModels;

internal sealed class CepViewModel : BasePageViewModel
{
    private CepDto? _CepDto;

    public bool HasCep { get => _CepDto is not null; }

    public string Logradouro { get { return _CepDto?.logradouro ?? string.Empty; } }

    public string Cep { get { return _CepDto?.cep ?? string.Empty; } }

    public string Complemento { get { return _CepDto?.complemento ?? string.Empty; } }

    public string Bairro { get { return _CepDto?.bairro ?? string.Empty; } }

    public string Localidade { get { return _CepDto?.localidade ?? string.Empty; } }

    public string UF { get { return _CepDto?.uf ?? string.Empty; } }

    internal override Task InitializeAsync(object? parameter)
    {
        _CepDto = parameter as CepDto ?? throw new ArgumentNullException(nameof(parameter));

        OnPropertyChanged(nameof(HasCep));
        OnPropertyChanged(nameof(Logradouro));
        OnPropertyChanged(nameof(Cep));
        OnPropertyChanged(nameof(Complemento));
        OnPropertyChanged(nameof(Bairro));
        OnPropertyChanged(nameof(Localidade));
        OnPropertyChanged(nameof(UF));

        return Task.CompletedTask;
    }
}
