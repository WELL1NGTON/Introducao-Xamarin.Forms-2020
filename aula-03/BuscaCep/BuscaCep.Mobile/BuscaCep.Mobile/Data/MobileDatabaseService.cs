using System;
using System.Linq;
using System.Linq.Expressions;

using BuscaCep.Mobile.Data.Dtos;
using BuscaCep.Mobile.Providers;

using SQLite;

using Xamarin.Forms;

namespace BuscaCep.Mobile.Data;

internal class MobileDatabaseService
{
    private static Lazy<MobileDatabaseService> _Lazy = new Lazy<MobileDatabaseService>(() => new MobileDatabaseService());
    public static MobileDatabaseService Current { get => _Lazy.Value; }

    private readonly SQLiteConnection _SQLiteConnection;

    public MobileDatabaseService()
    {
        var path = DependencyService.Get<IDatabasePathProvider>().GetPath();

        _SQLiteConnection = new SQLiteConnection(path);
        _SQLiteConnection.CreateTable<CepDto>();
    }

    public bool Save<TDto>(TDto dto) where TDto : new() => _SQLiteConnection.InsertOrReplace(dto) > 0;

    public TableQuery<TDto> Get<TDto>() where TDto : new() => _SQLiteConnection.Table<TDto>();

    public TDto Get<TDto>(Guid id) where TDto : new() => _SQLiteConnection.Find<TDto>(id);

    public TableQuery<TDto> Get<TDto>(Expression<Func<TDto, bool>> expression) where TDto : new()
        => _SQLiteConnection.Table<TDto>().Where(expression);
}

