using BuscaCep.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuscaCep.Mobile.Pages;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CepsPage : ContentPage
{
    CepsViewModel ViewModel { get => (CepsViewModel)BindingContext; }

    public CepsPage()
    {
        InitializeComponent();
    }

    void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item == null)
            return;

        ViewModel.SelecionarCommand.Execute(e.Item);

        //Deselect Item
        ((ListView)sender).SelectedItem = null;
    }
}
