using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BuscaCep.Mobile.ViewModels;

internal class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private bool _IsBusy = false;

    public bool IsBusy
    {
        get => _IsBusy;
        set
        {
            _IsBusy = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsNotBusy));
        }
    }

    public bool IsNotBusy { get => !_IsBusy; }
}

