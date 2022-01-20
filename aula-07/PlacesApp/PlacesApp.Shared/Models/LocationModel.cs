using System;

namespace PlacesApp.Models;

public sealed class LocationModel : BaseModel<Guid>
{
    public string Imagem { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public DateTime Data { get; set; } = DateTime.MinValue;

    public bool Favorito { get; set; } = false;
}

