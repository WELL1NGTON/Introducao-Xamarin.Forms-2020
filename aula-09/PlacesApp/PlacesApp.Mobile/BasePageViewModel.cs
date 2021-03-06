using System.Threading.Tasks;

namespace PlacesApp.Mobile;

internal abstract class BasePageViewModel : BaseViewModel
{
    public virtual Task Initialize(object? args = null) => Task.CompletedTask;
}

