using System;
using System.Text.RegularExpressions;

using SQLite;

namespace BuscaCep.Mobile.Data.Dtos;

[Table("Cep")]
public sealed class CepDto
{
    private string? _cep;

    [PrimaryKey]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? cep
    {
        get => _cep;
        set => _cep = string.IsNullOrWhiteSpace(value) ? value : Regex.Replace(value, @"[^\d]", string.Empty);
    }

    public string? logradouro { get; set; }

    public string? complemento { get; set; }

    public string? bairro { get; set; }

    public string? localidade { get; set; }

    public string? uf { get; set; }

    public string? ibge { get; set; }

    public string? gia { get; set; }

    public string? ddd { get; set; }

    public string? siafi { get; set; }

    public bool? erro { get; set; }

    [Ignore]
    public string Detalhes { get => $"{logradouro}, {bairro}, {localidade}/{uf}"; }
}

