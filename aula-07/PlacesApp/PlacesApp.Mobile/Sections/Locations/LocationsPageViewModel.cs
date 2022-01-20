using System.Collections.ObjectModel;
using System.Threading.Tasks;

using PlacesApp.Mobile.Clients;
using PlacesApp.Models;

namespace PlacesApp.Mobile.Sections.Locations;

internal sealed class LocationsPageViewModel : BasePageViewModel
{
    public LocationsPageViewModel()
    {
        Title = "Locations";
    }

    public ObservableCollection<LocationModel> Locations { get; private set; } = new();

    public override async Task InitializeAsync(object? args = null)
    {
        await base.InitializeAsync(args);

        foreach (var locationModel in PlacesClient.Current.GetLocations())
        {
            Locations.Add(locationModel);
        }
    }
}
