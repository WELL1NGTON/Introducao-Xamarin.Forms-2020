using System.Collections.ObjectModel;
using System.Threading.Tasks;

using MvvmHelpers.Commands;

using PlacesApp.Mobile.Clients;
using PlacesApp.Models;

namespace PlacesApp.Mobile.Sections.Locations;

internal sealed class LocationsPageViewModel : BasePageViewModel
{
    private LocationFilter _SelectedFilter = LocationFilter.Todos;

    private AsyncCommand<LocationModel>? _SelecionarCommand;

    public LocationsPageViewModel()
    {
        Title = "Locations";
    }

    public LocationFilter SelectedFilter
    {
        get => _SelectedFilter;
        set => SetProperty(ref _SelectedFilter, value);
    }

    public ObservableCollection<LocationModel> Locations { get; private set; } = new();


    public AsyncCommand<LocationModel> SelecionarCommand
        => _SelecionarCommand
        ??= new AsyncCommand<LocationModel>(
            execute: SelecionarCommandExecute,
            canExecute: SelecionarCommandCanExecute,
            onException: CommandOnException);

    private bool SelecionarCommandCanExecute(object arg) => true;

    private Task SelecionarCommandExecute(LocationModel location)
        => NavigationService.Navigate<LocationViewModel>(location);

    public override async Task Initialize(object? args = null)
    {
        await base.Initialize(args);

        foreach (var locationModel in PlacesClient.Current.GetLocations())
        {
            Locations.Add(locationModel);
        }
    }
}
