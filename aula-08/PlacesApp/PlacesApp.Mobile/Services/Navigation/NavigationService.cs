using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PlacesApp.Mobile.Sections.Locations;

using Xamarin.Forms;

namespace PlacesApp.Mobile.Services.Navigation;

internal sealed class NavigationService
{
    private static Lazy<NavigationService> _Lazy = new Lazy<NavigationService>(() => new());

    public static NavigationService Current { get => _Lazy.Value; }

    private readonly Dictionary<Type, Type> _Mappings;

    private NavigationService()
    {
        _Mappings = new();

        CreateViewModelMappings();
    }

    private Application CurrentApplication => App.Current;

    private INavigation Navigation { get => ((CustomNavigationPage)CurrentApplication.MainPage).Navigation; }

    private void CreateViewModelMappings()
    {
        _Mappings.Add(typeof(LocationsPageViewModel), typeof(LocationsPage));
    }

    internal Task Navigate<TViewModel>(object? parameter = null)
        where TViewModel : BasePageViewModel
        => InternalNavigate(typeof(TViewModel), parameter);

    private async Task InternalNavigate(Type viewModelType, object? parameter = null)
    {
        try
        {
            Page? page = null;

            // Verificar se estou indo para a página inicial...
            if (viewModelType == typeof(LocationsPageViewModel))
            {
                // Se a página inicial não foi carregada...
                if (!Navigation.NavigationStack.Any(lbda => lbda.BindingContext.GetType() == typeof(LocationsPageViewModel)))
                {
                    page = CreateAndBindPage(viewModelType);

                    var pagesToRemove = Navigation.NavigationStack.ToList();

                    await Navigation.PushAsync(page);

                    foreach (var pageToRemove in pagesToRemove)
                    {
                        Navigation.RemovePage(pageToRemove);
                    }
                }
                else
                {
                    await GoBack(toRoot: true);
                }
            }
            else
            {
                page = CreateAndBindPage(viewModelType);

                if (viewModelType.BaseType == typeof(BaseModalPageViewModel))
                {
                    await Navigation.PushModalAsync(page, true);
                }
                else
                {
                    await Navigation.PushAsync(page, true);
                }
            }

            if (page is not null)
            {
                await ((BasePageViewModel)page.BindingContext).InitializeAsync(parameter);
            }
        }
        catch
        {
            throw;
        }
    }

    public Task GoBack(bool toRoot, bool animated = true)
    {
        if (toRoot)
        {
            return Navigation.PopToRootAsync();
        }

        if (Navigation.NavigationStack.Count > 0)
        {
            return Navigation.PopModalAsync(animated);
        }

        return Navigation.PopAsync(animated);
    }

    private Type GetPageTypeForViewModel(Type viewModelType)
        => _Mappings.ContainsKey(viewModelType)
                ? _Mappings[viewModelType]
                : throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");

    private Page CreateAndBindPage(Type viewModelType)
    {
        try
        {
            var pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType is null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = (Page)Activator.CreateInstance(pageType);
            page.BindingContext = Activator.CreateInstance(viewModelType) as BasePageViewModel;

            return page;
        }
        catch
        {
            throw;
        }
    }

    internal void Initialize(object? args = null)
    {
        CurrentApplication.MainPage = new CustomNavigationPage();

        Device.BeginInvokeOnMainThread(async () =>
        {
            await Navigate<LocationsPageViewModel>(args);
        });
    }
}
