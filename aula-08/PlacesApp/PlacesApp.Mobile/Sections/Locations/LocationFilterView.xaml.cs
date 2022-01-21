using System;
using System.Windows.Input;

using PlacesApp.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlacesApp.Mobile.Sections.Locations;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LocationFilterView : ContentView
{
    public LocationFilterView()
    {
        InitializeComponent();
    }

    #region [ LocationFilter ]

    public LocationFilter LocationFilter
    {
        get { return (LocationFilter)GetValue(LocationFilterProperty); }
        set { SetValue(LocationFilterProperty, value); }
    }

    public static readonly BindableProperty LocationFilterProperty =
        BindableProperty.Create(
            nameof(LocationFilter),
            typeof(LocationFilter),
            typeof(LocationFilter),
            LocationFilter.Todos,
            propertyChanged: LocationFilterChanged);

    private static void LocationFilterChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var element = (LocationFilterView)bindable;

        element.txtNome.Text = ((LocationFilter)newValue).ToString();

        element.UpdateSelectedState();
    }

    #endregion

    #region [ SelectedFilter ]

    public LocationFilter SelectedFilter
    {
        get { return (LocationFilter)GetValue(SelectedFilterProperty); }
        set { SetValue(SelectedFilterProperty, value); }
    }

    public static readonly BindableProperty SelectedFilterProperty =
        BindableProperty.Create(
            nameof(SelectedFilter),
            typeof(LocationFilter),
            typeof(LocationFilter),
            LocationFilter.Todos,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: SelectedFilterChanged);

    private static void SelectedFilterChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var element = (LocationFilterView)bindable;

        //element.txtNome.Text = ((LocationFilters)newValue).ToString();

        element.UpdateSelectedState();
    }

    #endregion

    #region [ NormalColor ]

    [TypeConverter(typeof(ColorTypeConverter))]
    public Color NormalColor
    {
        get { return (Color)GetValue(NormalColorProperty); }
        set { SetValue(NormalColorProperty, value); }
    }

    public static readonly BindableProperty NormalColorProperty =
        BindableProperty.Create(
            nameof(NormalColor),
            typeof(Color),
            typeof(Color),
            default,
            propertyChanged: NormalColorChanged);

    private static void NormalColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var element = (LocationFilterView)bindable;

        //element.txtNome.Text = ((LocationFilters)newValue).ToString();

        element.UpdateSelectedState();
    }

    #endregion

    #region [ SelectedColor ]

    [TypeConverter(typeof(ColorTypeConverter))]
    public Color SelectedColor
    {
        get { return (Color)GetValue(SelectedColorProperty); }
        set { SetValue(SelectedColorProperty, value); }
    }

    public static readonly BindableProperty SelectedColorProperty =
        BindableProperty.Create(
            nameof(SelectedColor),
            typeof(Color),
            typeof(Color),
            default,
            propertyChanged: SelectedColorChanged);

    private static void SelectedColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var element = (LocationFilterView)bindable;

        //element.txtNome.Text = ((LocationFilters)newValue).ToString();

        element.UpdateSelectedState();
    }

    #endregion

    //#region [ SelectedCommand ]

    //public ICommand SelectedCommand
    //{
    //    get { return (ICommand)GetValue(SelectedCommandProperty); }
    //    set { SetValue(SelectedCommandProperty, value); }
    //}

    //public static readonly BindableProperty SelectedCommandProperty =
    //    BindableProperty.Create(
    //        propertyName: nameof(SelectedCommand),
    //        returnType: typeof(ICommand),
    //        declaringType: typeof(ICommand),
    //        defaultValue: null,
    //        propertyChanged: SelectedCommandChanged);

    //private static void SelectedCommandChanged(BindableObject bindable, object oldValue, object newValue)
    //{
    //}
    //#endregion

    void UpdateSelectedState()
    {
        if (LocationFilter == SelectedFilter)
        {
            containerSelectedIndicator.BackgroundColor = SelectedColor;
        }
        else
        {
            containerSelectedIndicator.BackgroundColor = NormalColor;
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        => SelectedFilter = LocationFilter; // SelectedCommand?.Execute(LocationFilter);
}
