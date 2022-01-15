using System;
using System.IO;

using BuscaCep.Mobile.Droid.Providers;
using BuscaCep.Mobile.Providers;

using Xamarin.Forms;

[assembly: Dependency(typeof(DroidDatabasePathProvider))]
namespace BuscaCep.Mobile.Droid.Providers;

internal class DroidDatabasePathProvider : IDatabasePathProvider
{
    public DroidDatabasePathProvider()
    {
    }

    public string GetPath()
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "BuscaCep.db3");
}
