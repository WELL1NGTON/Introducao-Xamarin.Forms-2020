using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using MvvmHelpers.Commands;

using PlacesApp.Mobile.Clients;
using PlacesApp.Models;

namespace PlacesApp.Mobile.Sections.Locations;

internal sealed class LocationsPageViewModel : BasePageViewModel
{
    private LocationFilter _SelectedFilter = LocationFilter.Todos;

    //private Command<LocationFilter>? _ChangedFilterCommand;

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

    //public Command<LocationFilter> ChangedFilterCommand
    //    => _ChangedFilterCommand
    //    ??= new Command<LocationFilter>(execute: ChangedFilterCommandExecute);

    //private void ChangedFilterCommandExecute(LocationFilter newFilter)
    //{
    //    if (SelectedFilter != newFilter)
    //    {
    //        SelectedFilter = newFilter;
    //    }
    //}

    public override async Task InitializeAsync(object? args = null)
    {
        await base.InitializeAsync(args);

        foreach (var locationModel in PlacesClient.Current.GetLocations())
        {
            Locations.Add(locationModel);
        }
    }
}
