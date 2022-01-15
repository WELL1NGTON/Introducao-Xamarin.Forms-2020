using Xamarin.Forms;
using BuscaCep.Mobile.ViewModels;

namespace BuscaCep.Mobile;

public partial class MainPage : ContentPage
{
    BuscaCepViewModel ViewModel { get => ((BuscaCepViewModel)BindingContext); }

    public MainPage()
    {
        InitializeComponent();
    }
}
