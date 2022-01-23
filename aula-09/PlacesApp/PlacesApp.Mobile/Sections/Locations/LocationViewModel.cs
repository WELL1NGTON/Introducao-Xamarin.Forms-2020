using System;
using System.Threading.Tasks;

using PlacesApp.Models;

namespace PlacesApp.Mobile.Sections.Locations;

internal sealed class LocationViewModel : BasePageViewModel
{
    private LocationModel? _Location;

    public LocationViewModel()
    {
    }

    public string? Imagem { get => _Location?.Imagem; }

    public string Nome { get => _Location?.Nome ?? string.Empty; }

    public string Descricao { get => _Location?.Descricao ?? string.Empty; }

    public DateTime Data { get => _Location?.Data ?? DateTime.MinValue; }

    public override async Task Initialize(object? args = null)
    {
        await base.Initialize(args);

        _Location = args switch
        {
            LocationModel location => location,
            _ => throw new InvalidOperationException(message: "Algo de errado não deu certo ao carregar o local"),
        };

        OnPropertyChanged(nameof(Imagem));
        OnPropertyChanged(nameof(Nome));
        OnPropertyChanged(nameof(Descricao));
        OnPropertyChanged(nameof(Data));
    }
}
