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
    /// Creates a calendar.
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="calendarDisplayName">The display name of the calendar.</param>
    /// <param name="calendarDescription">The description of the calendar.</param>
    /// <param name="calendarColor">The color of the calendar.</param>
    /// <param name="isHiddenInCalendar">A value indicating whether the calendar is hidden from the calendar view or not.</param>
    /// <param name="isHiddenInList">A value indicating whether the calendar is hidden from the calendar list or not.</param>
    /// <param name="isEvent">A value indicating whether the calendar contains events or not.</param>
    /// <param name="isTodo">A value indicating whether the calendar contains tasks or not.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameters are invalid.</exception>
    public async Task<Result<CalendarResult, CommonErrorCode>?> CreateCalendar( 
        int apiVersion,
        string calendarDisplayName,
        string? calendarDescription = null,
        string? calendarColor = null,
        bool isHiddenInCalendar = false,
        bool isHiddenInList = false,
        bool isEvent = false,
        bool isTodo = false)
    {
        // Some checks.
        if (apiVersion < 2)
        {
            throw new ArgumentException(nameof(apiVersion), "The API version must be bigger than or equal to 2.");
        }

        if (string.IsNullOrWhiteSpace(calendarDisplayName))
        {
            throw new ArgumentException(nameof(calendarDisplayName), "The calendar display name must not be empty.");
        }

        // Fill the parameters.
        var parameters = new QueryParameters();

        if (apiVersion >= 2)
        {
            parameters.AddIfNotNull(HeaderKeys.CalendarDisplayName, calendarDisplayName);
            parameters.AddIfNotNull(HeaderKeys.CalendarDescription, calendarDescription);
            parameters.AddIfNotNull(HeaderKeys.CalendarDisplayName, calendarColor);
            parameters.AddIfNotNull(HeaderKeys.IsHiddenInCalendar, isHiddenInCalendar.ToString());
            parameters.AddIfNotNull(HeaderKeys.IsHiddenInList, isHiddenInList.ToString());
            parameters.AddIfNotNull(HeaderKeys.IsEvent, isEvent.ToString());
            parameters.AddIfNotNull(HeaderKeys.IsTodo, isTodo.ToString());
        }

        var paramString = parameters.ToString();
        var queryString = string.IsNullOrWhiteSpace(paramString) ?
            string.Format(ApiEndpoints.CreateCalendar, apiVersion) : $"{string.Format(ApiEndpoints.CreateCalendar, apiVersion)}&{parameters}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, queryString);
        httpRequest.Headers.TryAddWithoutValidation(HeaderKeys.SynoToken, this.SynoToken);
        var response = await this.httpClient.SendAsync(httpRequest);
        var resultString = await response.Content.ReadAsStringAsync();

        try
        {
            return JsonConvert.DeserializeObject<Result<CalendarResult, CommonErrorCode>?>(resultString);
        }
        catch(Exception ex)
        {
            this.Logger.Error(ex, "An error occured when deserializing the API data.");
            return null;
        }
    }

    /// <summary>
    /// Gets all calendars.
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="isEvent">A value indicating whether the calendar contains events or not.</param>
    /// <param name="isTodo">A value indicating whether the calendar contains tasks or not.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameters are invalid.</exception>
    public async Task<Result<List<CalendarResult>, CommonErrorCode>?> GetAllCalendars(
        int apiVersion,
        bool isEvent = true,
        bool isTodo = true)
    {
        // Some checks.
        if (apiVersion < 2)
        {
            throw new ArgumentException(nameof(apiVersion), "The API version must be bigger than or equal to 2.");
        }

        // Fill the parameters.
        var parameters = new QueryParameters();

        if (apiVersion >= 2)
        {
            parameters.AddIfNotNull(HeaderKeys.IsEvent, isEvent.ToString());
            parameters.AddIfNotNull(HeaderKeys.IsTodo, isTodo.ToString());
        }

        var paramString = parameters.ToString();
        var queryString = string.IsNullOrWhiteSpace(paramString) ?
            string.Format(ApiEndpoints.GetAllCalendars, apiVersion) : $"{string.Format(ApiEndpoints.GetAllCalendars, apiVersion)}&{parameters}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, queryString);
        httpRequest.Headers.TryAddWithoutValidation(HeaderKeys.SynoToken, this.SynoToken);
        var response = await this.httpClient.SendAsync(httpRequest);
        var resultString = await response.Content.ReadAsStringAsync();

        try
        {
            return JsonConvert.DeserializeObject<Result<List<CalendarResult>, CommonErrorCode>?> (resultString);
        }
        catch (Exception ex)
        {
            this.Logger.Error(ex, "An error occured when deserializing the API data.");
            return null;
        }
    }

    /// <summary>
    /// Gets a calendar by its identifier.
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="calendarId">The calendar identifier.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameters are invalid.</exception>
    public async Task<Result<CalendarResult, CommonErrorCode>?> GetCalendarById(
        int apiVersion,
        string calendarId)
    {
        // Some checks.
        if (apiVersion < 2)
        {
            throw new ArgumentException(nameof(apiVersion), "The API version must be bigger than or equal to 2.");
        }

        if (string.IsNullOrWhiteSpace(calendarId))
        {
            throw new ArgumentException(nameof(calendarId), "The calendar identifier must not be empty.");
        }

        // Fill the parameters.
        var parameters = new QueryParameters();

        if (apiVersion >= 2)
        {
            parameters.AddIfNotNull(HeaderKeys.CalendarId, calendarId);
        }

        var paramString = parameters.ToString();
        var queryString = string.IsNullOrWhiteSpace(paramString) ?
            string.Format(ApiEndpoints.GetCalendarById, apiVersion) : $"{string.Format(ApiEndpoints.GetCalendarById, apiVersion)}&{parameters}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, queryString);
        httpRequest.Headers.TryAddWithoutValidation(HeaderKeys.SynoToken, this.SynoToken);
        var response = await this.httpClient.SendAsync(httpRequest);
        var resultString = await response.Content.ReadAsStringAsync();

        try
        {
            return JsonConvert.DeserializeObject<Result<CalendarResult, CommonErrorCode>?> (resultString);
        }
        catch (Exception ex)
        {
            this.Logger.Error(ex, "An error occured when deserializing the API data.");
            return null;
        }
    }

    /// <summary>
    /// Sets some calendar data.
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="calendarId">The calendar identifier.</param>
    /// <param name="calendarDisplayName">The display name of the calendar.</param>
    /// <param name="calendarColor">The calendar color.</param>
    /// <param name="isHiddenInList">A value indicating whether the calendar is hidden from the calendar list or not.</param>
    /// <param name="isHiddenInCalendar">A value indicating whether the calendar is hidden from the calendar view or not.</param>
    /// <param name="calendarDescription">The description of the calendar.</param>
    /// <param name="calendarExtraInformation">The extra information for the calendar.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameters are invalid.</exception>
    public async Task<Result<CalendarResult, CommonErrorCode>?> SetCalendarData(
        int apiVersion,
        string calendarId,
        string calendarDisplayName,
        string? calendarColor = null,
        bool isHiddenInList = false,
        bool isHiddenInCalendar = false,
        string? calendarDescription = null,
        string? calendarExtraInformation = null)
    {
        // Some checks.
        if (apiVersion < 2)
        {
            throw new ArgumentException(nameof(apiVersion), "The API version must be bigger than or equal to 2.");
        }

        if (string.IsNullOrWhiteSpace(calendarId))
        {
            throw new ArgumentException(nameof(calendarId), "The calendar identifier must not be empty.");
        }

        if (string.IsNullOrWhiteSpace(calendarDisplayName))
        {
            throw new ArgumentException(nameof(calendarDisplayName), "The calendar display name must not be empty.");
        }

        // Fill the parameters.
        var parameters = new QueryParameters();

        if (apiVersion >= 2)
        {
            parameters.AddIfNotNull(HeaderKeys.CalendarId, calendarId);
            parameters.AddIfNotNull(HeaderKeys.CalendarColor, calendarColor);
            parameters.AddIfNotNull(HeaderKeys.IsHiddenInList, isHiddenInList.ToString());
            parameters.AddIfNotNull(HeaderKeys.IsHiddenInCalendar, isHiddenInCalendar.ToString());
            parameters.AddIfNotNull(HeaderKeys.CalendarDisplayName, calendarDisplayName);
            parameters.AddIfNotNull(HeaderKeys.CalendarDescription, calendarDescription);
            parameters.AddIfNotNull(HeaderKeys.CalendarExtraInformation, calendarExtraInformation);
        }

        var paramString = parameters.ToString();
        var queryString = string.IsNullOrWhiteSpace(paramString) ?
            string.Format(ApiEndpoints.SetCalendarData, apiVersion) : $"{string.Format(ApiEndpoints.SetCalendarData, apiVersion)}&{parameters}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, queryString);
        httpRequest.Headers.TryAddWithoutValidation(HeaderKeys.SynoToken, this.SynoToken);
        var response = await this.httpClient.SendAsync(httpRequest);
        var resultString = await response.Content.ReadAsStringAsync();

        try
        {
            return JsonConvert.DeserializeObject<Result<CalendarResult, CommonErrorCode>?> (resultString);
        }
        catch (Exception ex)
        {
            this.Logger.Error(ex, "An error occured when deserializing the API data.");
            return null;
        }
    }

    /// <summary>
    /// Deletes a calendar by its identifier.
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="calendarId">The calendar identifier.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameters are invalid.</exception>
    public async Task<Result<CalendarResult, CommonErrorCode>?> DeleteCalendarById(
        int apiVersion,
        string calendarId)
    {
        // Some checks.
        if (apiVersion < 2)
        {
            throw new ArgumentException(nameof(apiVersion), "The API version must be bigger than or equal to 2.");
        }

        if (string.IsNullOrWhiteSpace(calendarId))
        {
            throw new ArgumentException(nameof(calendarId), "The calendar identifier must not be empty.");
        }

        // Fill the parameters.
        var parameters = new QueryParameters();

        if (apiVersion >= 2)
        {
            parameters.AddIfNotNull(HeaderKeys.CalendarId, calendarId);
        }

        var paramString = parameters.ToString();
        var queryString = string.IsNullOrWhiteSpace(paramString) ?
            string.Format(ApiEndpoints.DeleteCalendarById, apiVersion) : $"{string.Format(ApiEndpoints.DeleteCalendarById, apiVersion)}&{parameters}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, queryString);
        httpRequest.Headers.TryAddWithoutValidation(HeaderKeys.SynoToken, this.SynoToken);
        var response = await this.httpClient.SendAsync(httpRequest);
        var resultString = await response.Content.ReadAsStringAsync();

        try
        {
            return JsonConvert.DeserializeObject<Result<CalendarResult, CommonErrorCode>?>(resultString);
        }
        catch (Exception ex)
        {
            this.Logger.Error(ex, "An error occured when deserializing the API data.");
            return null;
        }
    }
}
