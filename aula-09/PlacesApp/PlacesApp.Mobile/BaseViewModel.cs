using System;

using MvvmHelpers.Commands;

using PlacesApp.Mobile.Services.Navigation;

using MvvmHelpersBaseViewModel = MvvmHelpers.BaseViewModel;

namespace PlacesApp.Mobile;

internal abstract class BaseViewModel : MvvmHelpersBaseViewModel
{
    private AsyncCommand? _GoBackCommand;

    protected NavigationService NavigationService => NavigationService.Current;

    public AsyncCommand GoBackCommand
        => _GoBackCommand ??= new AsyncCommand(
            execute: () => NavigationService.GoBack(),
            onException: CommandOnException);

    protected void CommandOnException(Exception obj)
    {
        throw new NotImplementedException();
    }
}
