using System;
using System.IO;

using BuscaCep.Mobile.iOS.Providers;
using BuscaCep.Mobile.Providers;

using Xamarin.Forms;

[assembly: Dependency(typeof(IosDatabasePathProvider))]
namespace BuscaCep.Mobile.iOS.Providers;

internal class IosDatabasePathProvider : IDatabasePathProvider
{
    public IosDatabasePathProvider()
    {
    }

    public string GetPath()
    {
        var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", "Library", "Databases");

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        return Path.Combine(folder, "BuscaCep.db3");
    }
}
