﻿using PlacesApp.Mobile.Services.Navigation;

using Xamarin.Forms;

namespace PlacesApp.Mobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        NavigationService.Current.Initialize();
    }

    protected override void OnStart()
    {
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }
}