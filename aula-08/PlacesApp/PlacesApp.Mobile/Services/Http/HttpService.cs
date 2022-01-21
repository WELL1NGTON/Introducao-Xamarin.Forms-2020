using System;
using System.Collections.Generic;
using System.Net.Http;

using PlacesApp.Models;

namespace PlacesApp.Mobile.Services.Http;

internal sealed class HttpService
{
    private static Lazy<HttpService> _Lazy = new Lazy<HttpService>(() => new HttpService());

    private HttpClient _HttpClient;

    private HttpService()
    {
        _HttpClient = new HttpClient();
    }

    public static HttpService Current => _Lazy.Value;
}
