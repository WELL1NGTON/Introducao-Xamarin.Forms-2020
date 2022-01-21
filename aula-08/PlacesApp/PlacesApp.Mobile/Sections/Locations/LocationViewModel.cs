using System.Threading.Tasks;

using PlacesApp.Models;

namespace PlacesApp.Mobile.Sections.Locations;

internal sealed class LocationViewModel : BasePageViewModel
{
    public LocationViewModel(LocationModel model)
    {
        Location = model;
    }

    public LocationModel Location;

    public override Task InitializeAsync(object? args = null)
    {
        return base.InitializeAsync(args);
    }
}
