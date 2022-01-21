using System;
using System.Collections.Generic;

using PlacesApp.Models;

namespace PlacesApp.Mobile.Clients;

internal sealed class PlacesClient
{
    private static Lazy<PlacesClient> _Lazy = new Lazy<PlacesClient>(() => new PlacesClient());

    public static PlacesClient Current => _Lazy.Value;

    private PlacesClient()
    {

    }

    public List<LocationModel> GetLocations()
        => new List<LocationModel>
        {
            new LocationModel
            {
                Data = DateTime.Now,
                DataInclusao = DateTime.Now,
                DataUltimaAlteracao = DateTime.Now,
                Descricao = "O melhor doce de leite EVER",
                Favorito = true,
                Id = Guid.NewGuid(),
                Imagem = "https://picsum.photos/200/300?random=1",
                Nome = "Viçosa/MG",
            },
            new LocationModel
            {
                Data = DateTime.Now,
                DataInclusao = DateTime.Now,
                DataUltimaAlteracao = DateTime.Now,
                Descricao = "Cidade onde Balivo reside",
                Favorito = true,
                Id = Guid.NewGuid(),
                Imagem = "https://picsum.photos/200/300?random=2",
                Nome = "Jaú/SP",
            },
            new LocationModel
            {
                Data = DateTime.Now,
                DataInclusao = DateTime.Now,
                DataUltimaAlteracao = DateTime.Now,
                Descricao = "São Paulo... Porque é SP...",
                Favorito = true,
                Id = Guid.NewGuid(),
                Imagem = "https://picsum.photos/200/300?random=3",
                Nome = "São Paulo/SP",
            },
        };
}
