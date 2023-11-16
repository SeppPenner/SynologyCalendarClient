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
    /// Gets the API information (Which APIs are supported).
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="query">The query.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameter is invalid.</exception>
    public async Task<Result<Dictionary<string, InfoResult>, CommonErrorCode>?> GetApiInformation(
        int apiVersion,
        string query)
    {
        // Some checks.
        if (apiVersion < 1)
        {
            throw new ArgumentException(nameof(apiVersion), "The API version must be bigger than or equal to 1.");
        }

        if (string.IsNullOrWhiteSpace(query))
        {
            throw new ArgumentException(nameof(query), "The query must not be empty.");
        }

        // Fill the parameters.
        var parameters = new QueryParameters();

        if (apiVersion >= 1)
        {
            parameters.AddIfNotNull(HeaderKeys.Query, query);
        }

        var paramString = parameters.ToString();
        var queryString = string.IsNullOrWhiteSpace(paramString) ?
            string.Format(ApiEndpoints.GetApiInformation, apiVersion) : $"{string.Format(ApiEndpoints.GetApiInformation, apiVersion)}&{parameters}";
        var response = await this.httpClient.GetAsync(queryString);
        var resultString = await response.Content.ReadAsStringAsync();

        try
        {
            return JsonConvert.DeserializeObject<Result<Dictionary<string, InfoResult>, CommonErrorCode>?>(resultString);
        }
        catch(Exception ex)
        {
            this.Logger.Error(ex, "An error occured when deserializing the API data.");
            return null;
        }
    }
}
