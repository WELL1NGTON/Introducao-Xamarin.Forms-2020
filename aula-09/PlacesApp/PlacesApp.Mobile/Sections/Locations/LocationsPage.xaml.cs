
using MvvmHelpers;

using PlacesApp.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlacesApp.Mobile.Sections.Locations;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LocationsPage : ContentPage
{
    public LocationsPage()
    {
        InitializeComponent();
    }

    LocationsPageViewModel ViewModel => (LocationsPageViewModel)BindingContext;

    private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item == null)
        {
            return;
        }

        ViewModel
            .SelecionarCommand
            .ExecuteAsync((LocationModel)e.Item)
            .SafeFireAndForget();

        // Deselect Item
        ((ListView)sender).SelectedItem = null;
    }
}
