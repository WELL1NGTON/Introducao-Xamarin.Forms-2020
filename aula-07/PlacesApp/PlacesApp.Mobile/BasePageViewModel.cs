using System.Threading.Tasks;

namespace PlacesApp.Mobile;

internal abstract class BasePageViewModel : BaseViewModel
{
    public virtual Task InitializeAsync(object? args = null) => Task.CompletedTask;
}

