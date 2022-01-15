using System;
using System.Threading.Tasks;

namespace BuscaCep.Mobile.ViewModels;

internal abstract class BasePageViewModel : BaseViewModel
{
    internal abstract Task InitializeAsync(object? parameter);
}

