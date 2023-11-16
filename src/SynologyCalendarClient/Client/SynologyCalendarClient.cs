// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynologyCalendarClient.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The main Synology calendar API client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Client;

/// <summary>
/// The main Synology calendar API client.
/// </summary>
public partial class SynologyCalendarClient
{
    /// <summary>
    /// The HTTP client.
    /// </summary>
    private readonly HttpClient httpClient = new();

    /// <summary>
    /// Gets or sets the logger.
    /// </summary>
    protected ILogger Logger { get; set; }

    /// <summary>
    /// Gets or sets the Synology token per request.
    /// </summary>
    protected string SynoToken { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="SynologyCalendarClient"/> class.
    /// </summary>
    /// <param name="baseUrl">The base url.</param>
    /// <param name="logger">The logger.</param>
    /// <exception cref="ArgumentNullException">Thrown if the API version is invalid.</exception>
    public SynologyCalendarClient(string baseUrl, ILogger logger)
    {
        // Add the cookie container and the HTTP handler to the HTTP client.
        var cookieContainer = new CookieContainer();

        var handler = new HttpClientHandler
        {
            SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13,
            CookieContainer = cookieContainer
        };

        this.httpClient = new(handler)
        {
            BaseAddress = new Uri(baseUrl)
        };

        this.Logger = logger;
    }
}
