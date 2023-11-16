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
    /// Creates an event.
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="calendarId">The calendar identifier.</param>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date.</param>
    /// <param name="isAllDay">A value indicating whether the event is an all day event or not.</param>
    /// <param name="originalCalendarId">The original calendar identifier.</param>
    /// <param name="summary">The summary.</param>
    /// <param name="description">The description.</param>
    /// <param name="attendees">The attendees.</param>
    /// <param name="notifications">The notifications.</param>
    /// <param name="isRepeatEvent">A value indicating whether the event is repeated or not.</param>
    /// <param name="eventLocation">The eventLocation.</param>
    /// <param name="transparent">The transparent settings.</param>
    /// <param name="timeZoneId">The time zone identifier.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameters are invalid.</exception>
    public async Task<Result<EventResult, CommonErrorCode>?> CreateEvent( 
        int apiVersion,
        string calendarId,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        bool isAllDay,
        string originalCalendarId,
        string summary,
        string? description = null,
        List<EventAttendee>? attendees = null,
        List<EventNotification>? notifications = null,
        bool isRepeatEvent = false,
        string? eventLocation = null,
        string? transparent = null,
        string? timeZoneId = null)
    {
        // Some checks.
        if (apiVersion < 1)
        {
            throw new ArgumentException(nameof(apiVersion), "The API version must be bigger than or equal to 1.");
        }

        if (string.IsNullOrWhiteSpace(calendarId))
        {
            throw new ArgumentException(nameof(calendarId), "The calendar identifier must not be empty.");
        }

        if (startDate == DateTimeOffset.MinValue || startDate == DateTimeOffset.MaxValue)
        {
            throw new ArgumentException(nameof(startDate), "The start date must not be a default value.");
        }

        if (endDate == DateTimeOffset.MinValue || endDate == DateTimeOffset.MaxValue)
        {
            throw new ArgumentException(nameof(endDate), "The end date must not be a default value.");
        }

        if (string.IsNullOrWhiteSpace(originalCalendarId))
        {
            throw new ArgumentException(nameof(originalCalendarId), "The original calendar identifier must not be empty.");
        }

        if (string.IsNullOrWhiteSpace(summary))
        {
            throw new ArgumentException(nameof(summary), "The summary must not be empty.");
        }

        // Fill the parameters.
        var parameters = new QueryParameters();

        if (apiVersion >= 1)
        {
            parameters.AddIfNotNull(HeaderKeys.CalendarId, calendarId);
            parameters.AddIfNotNull(HeaderKeys.Description, description);
            parameters.AddIfNotNull(HeaderKeys.StartDate, $"TZID={timeZoneId}:{startDate:yyyyMMddTHHmmss}");
            parameters.AddIfNotNull(HeaderKeys.EndDate, $"TZID={timeZoneId}:{endDate:yyyyMMddTHHmmss}");
            // Todo: Check if as JSON?
            parameters.AddIfNotNull(HeaderKeys.EventAttendees, attendees);
            // Todo: Check if as JSON?
            parameters.AddIfNotNull(HeaderKeys.EventNotifications, notifications);
            parameters.AddIfNotNull(HeaderKeys.IsAllDay, isAllDay.ToString());
            parameters.AddIfNotNull(HeaderKeys.IsRepeatEvent, isRepeatEvent.ToString());
            parameters.AddIfNotNull(HeaderKeys.EventLocation, eventLocation);
            parameters.AddIfNotNull(HeaderKeys.OriginalCalendarId, originalCalendarId);
            parameters.AddIfNotNull(HeaderKeys.Summary, summary);
            parameters.AddIfNotNull(HeaderKeys.Transparent, transparent);
            parameters.AddIfNotNull(HeaderKeys.TimezoneId, timeZoneId);
        }

        var paramString = parameters.ToString();
        var queryString = string.IsNullOrWhiteSpace(paramString) ?
            string.Format(ApiEndpoints.CreateEvent, apiVersion) : $"{string.Format(ApiEndpoints.CreateEvent, apiVersion)}&{parameters}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, queryString);
        httpRequest.Headers.TryAddWithoutValidation(HeaderKeys.SynoToken, this.SynoToken);
        var response = await this.httpClient.SendAsync(httpRequest);
        var resultString = await response.Content.ReadAsStringAsync();

        try
        {
            return JsonConvert.DeserializeObject<Result<EventResult, CommonErrorCode>?>(resultString);
        }
        catch(Exception ex)
        {
            this.Logger.Error(ex, "An error occured when deserializing the API data.");
            return null;
        }
    }

    /// <summary>
    /// Gets all events.
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="calendarIdList">The calendar identifier list.</param>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date.</param>
    /// <param name="listRepeat">A value indicating whether the events are repeated in the list or not.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameters are invalid.</exception>
    public async Task<Result<Dictionary<string, List<EventResult>>, CommonErrorCode>?> GetAllEvents(
        int apiVersion,
        List<string> calendarIdList,
        DateTimeOffset? startDate = null,
        DateTimeOffset? endDate = null,
        bool? listRepeat = null)
    {
        // Some checks.
        if (apiVersion < 1)
        {
            throw new ArgumentException(nameof(apiVersion), "The API version must be bigger than or equal to 1.");
        }

        if (calendarIdList.IsEmptyOrNull())
        {
            throw new ArgumentException(nameof(calendarIdList), "The calendar identifier list must not be empty.");
        }

        // Fill the parameters.
        var parameters = new QueryParameters();

        if (apiVersion >= 1)
        {
            parameters.AddIfNotNull(HeaderKeys.CalendarIdentifierList, calendarIdList);
            parameters.AddIfNotNull(HeaderKeys.StartDate, startDate?.ToString("yyyyMMdd"));
            parameters.AddIfNotNull(HeaderKeys.EndDate, endDate?.ToString("yyyyMMdd"));
            parameters.AddIfNotNull(HeaderKeys.ListRepeat, listRepeat?.ToString());
        }

        var paramString = parameters.ToString();
        var queryString = string.IsNullOrWhiteSpace(paramString) ?
            string.Format(ApiEndpoints.GetAllEvents, apiVersion) : $"{string.Format(ApiEndpoints.GetAllEvents, apiVersion)}&{parameters}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, queryString);
        httpRequest.Headers.TryAddWithoutValidation(HeaderKeys.SynoToken, this.SynoToken);
        var response = await this.httpClient.SendAsync(httpRequest);
        var resultString = await response.Content.ReadAsStringAsync();

        try
        {
            return JsonConvert.DeserializeObject<Result<Dictionary<string, List<EventResult>>, CommonErrorCode>?> (resultString);
        }
        catch (Exception ex)
        {
            this.Logger.Error(ex, "An error occured when deserializing the API data.");
            return new();
        }
    }

    /// <summary>
    /// Gets an event by its identifier.
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="eventId">The event identifier.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameters are invalid.</exception>
    public async Task<Result<EventResult, CommonErrorCode>?> GetEventById(
        int apiVersion,
        string eventId)
    {
        // Some checks.
        if (apiVersion < 1)
        {
            throw new ArgumentException(nameof(apiVersion), "The API version must be bigger than or equal to 1.");
        }

        if (string.IsNullOrWhiteSpace(eventId))
        {
            throw new ArgumentException(nameof(eventId), "The event identifier must not be empty.");
        }

        // Fill the parameters.
        var parameters = new QueryParameters();

        if (apiVersion >= 1)
        {
            parameters.AddIfNotNull(HeaderKeys.EventId, eventId);
        }

        var paramString = parameters.ToString();
        var queryString = string.IsNullOrWhiteSpace(paramString) ?
            string.Format(ApiEndpoints.GetEventById, apiVersion) : $"{string.Format(ApiEndpoints.GetEventById, apiVersion)}&{parameters}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, queryString);
        httpRequest.Headers.TryAddWithoutValidation(HeaderKeys.SynoToken, this.SynoToken);
        var response = await this.httpClient.SendAsync(httpRequest);
        var resultString = await response.Content.ReadAsStringAsync();

        try
        {
            return JsonConvert.DeserializeObject<Result<EventResult, CommonErrorCode>?> (resultString);
        }
        catch (Exception ex)
        {
            this.Logger.Error(ex, "An error occured when deserializing the API data.");
            return null;
        }
    }

    /// <summary>
    /// Sets some event data.
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="eventId">The event identifier.</param>
    /// <param name="calendarId">The calendar identifier.</param>
    /// <param name="description">The description.</param>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date.</param>
    /// <param name="attendees">The attendees.</param>
    /// <param name="notifications">The notifications.</param>
    /// <param name="isAllDay">A value indicating whether the event is an all day event or not.</param>
    /// <param name="isRepeatEvent">A value indicating whether the event is repeated or not.</param>
    /// <param name="eventLocation">The eventLocation.</param>
    /// <param name="originalCalendarId">The original calendar identifier.</param>
    /// <param name="summary">The summary.</param>
    /// <param name="transparent">The transparent settings.</param>
    /// <param name="timeZoneId">The time zone identifier.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameters are invalid.</exception>
    public async Task<Result<EventResult, CommonErrorCode>?> SetEventData(
        int apiVersion,
        string eventId,
        string? calendarId = null,
        string? description = null,
        DateTimeOffset? startDate = null,
        DateTimeOffset? endDate = null,
        List<EventAttendee>? attendees = null,
        List<EventNotification>? notifications = null,
        bool? isAllDay = null,
        bool? isRepeatEvent = null,
        string? eventLocation = null,
        string? originalCalendarId = null,
        string? summary = null,
        string? transparent = null,
        string? timeZoneId = null)
    {
        // Some checks.
        if (apiVersion < 1)
        {
            throw new ArgumentException(nameof(apiVersion), "The API version must be bigger than or equal to 1.");
        }

        if (string.IsNullOrWhiteSpace(eventId))
        {
            throw new ArgumentException(nameof(eventId), "The event identifier must not be empty.");
        }

        // Fill the parameters.
        var parameters = new QueryParameters();

        if (apiVersion >= 1)
        {
            parameters.AddIfNotNull(HeaderKeys.CalendarId, calendarId);
            parameters.AddIfNotNull(HeaderKeys.Description, description);
            parameters.AddIfNotNull(HeaderKeys.StartDate, startDate is null ? null : $"TZID={timeZoneId}:{startDate?.ToString("yyyyMMddTHHmmss")}");
            parameters.AddIfNotNull(HeaderKeys.EndDate, endDate is null ? null : $"TZID={timeZoneId}:{endDate?.ToString("yyyyMMddTHHmmss")}");
            // Todo: Check if as JSON?
            parameters.AddIfNotNull(HeaderKeys.EventAttendees, attendees);
            parameters.AddIfNotNull(HeaderKeys.EventId, eventId);
            // Todo: Check if as JSON?
            parameters.AddIfNotNull(HeaderKeys.EventNotifications, notifications);
            parameters.AddIfNotNull(HeaderKeys.IsAllDay, isAllDay.ToString());
            parameters.AddIfNotNull(HeaderKeys.IsRepeatEvent, isRepeatEvent.ToString());
            parameters.AddIfNotNull(HeaderKeys.EventLocation, eventLocation);
            parameters.AddIfNotNull(HeaderKeys.OriginalCalendarId, originalCalendarId);
            parameters.AddIfNotNull(HeaderKeys.Summary, summary);
            parameters.AddIfNotNull(HeaderKeys.Transparent, transparent);
            parameters.AddIfNotNull(HeaderKeys.TimezoneId, timeZoneId);
        }

        var paramString = parameters.ToString();
        var queryString = string.IsNullOrWhiteSpace(paramString) ?
            string.Format(ApiEndpoints.SetEventData, apiVersion) : $"{string.Format(ApiEndpoints.SetEventData, apiVersion)}&{parameters}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, queryString);
        httpRequest.Headers.TryAddWithoutValidation(HeaderKeys.SynoToken, this.SynoToken);
        var response = await this.httpClient.SendAsync(httpRequest);
        var resultString = await response.Content.ReadAsStringAsync();

        try
        {
            return JsonConvert.DeserializeObject<Result<EventResult, CommonErrorCode>?> (resultString);
        }
        catch (Exception ex)
        {
            this.Logger.Error(ex, "An error occured when deserializing the API data.");
            return null;
        }
    }

    /// <summary>
    /// Deletes an event by its identifier.
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="eventId">The event identifier.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameters are invalid.</exception>
    public async Task<Result<EventResult, CommonErrorCode>?> DeleteEventById(
        int apiVersion,
        string eventId)
    {
        // Some checks.
        if (apiVersion < 1)
        {
            throw new ArgumentException(nameof(apiVersion), "The API version must be bigger than or equal to 1.");
        }

        if (string.IsNullOrWhiteSpace(eventId))
        {
            throw new ArgumentException(nameof(eventId), "The event identifier must not be empty.");
        }

        // Fill the parameters.
        var parameters = new QueryParameters();

        if (apiVersion >= 1)
        {
            parameters.AddIfNotNull(HeaderKeys.EventId, eventId);
        }

        var paramString = parameters.ToString();
        var queryString = string.IsNullOrWhiteSpace(paramString) ?
            string.Format(ApiEndpoints.DeleteEventById, apiVersion) : $"{string.Format(ApiEndpoints.DeleteEventById, apiVersion)}&{parameters}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, queryString);
        httpRequest.Headers.TryAddWithoutValidation(HeaderKeys.SynoToken, this.SynoToken);
        var response = await this.httpClient.SendAsync(httpRequest);
        var resultString = await response.Content.ReadAsStringAsync();

        try
        {
            return JsonConvert.DeserializeObject<Result<EventResult, CommonErrorCode>?>(resultString);
        }
        catch (Exception ex)
        {
            this.Logger.Error(ex, "An error occured when deserializing the API data.");
            return null;
        }
    }
}
