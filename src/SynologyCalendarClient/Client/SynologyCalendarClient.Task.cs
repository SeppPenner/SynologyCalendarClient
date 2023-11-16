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
    /// Creates a task.
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="calendarId">The calendar identifier.</param>
    /// <param name="originalCalendarId">The original calendar identifier.</param>
    /// <param name="summary">The summary.</param>
    /// <param name="timeZoneId">The time zone identifier.</param>
    /// <param name="notifications">The notifications.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameters are invalid.</exception>
    public async Task<Result<EventResult, CommonErrorCode>?> CreateTask( 
        int apiVersion,
        string calendarId,
        string originalCalendarId,
        string summary,
        string? timeZoneId = null,
        List<EventNotification>? notifications = null)
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
            parameters.AddIfNotNull(HeaderKeys.OriginalCalendarId, originalCalendarId);
            parameters.AddIfNotNull(HeaderKeys.Summary, summary);
            parameters.AddIfNotNull(HeaderKeys.TimezoneId, timeZoneId);
            parameters.AddIfNotNull(HeaderKeys.EventNotifications, notifications);
        }

        var paramString = parameters.ToString();
        var queryString = string.IsNullOrWhiteSpace(paramString) ?
            string.Format(ApiEndpoints.CreateTask, apiVersion) : $"{string.Format(ApiEndpoints.CreateTask, apiVersion)}&{parameters}";
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
    /// Gets all tasks.
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="calendarIdList">The calendar identifier list.</param>
    /// <param name="limit">The limit.</param>
    /// <param name="offset">The offset.</param>
    /// <param name="filterDue">A value indicating whether due events are filtered or not.</param>
    /// <param name="filterComplete">A value indicating whether completed events are filtered or not.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameters are invalid.</exception>
    public async Task<Result<Dictionary<string, List<EventResult>>, CommonErrorCode>?> GetAllTasks(
        int apiVersion,
        List<string> calendarIdList,
        int limit = 0,
        int offset = 0,
        FilterDue filterDue = FilterDue.ShowBoth,
        FilterComplete filterComplete = FilterComplete.ShowBoth)
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
            parameters.AddIfNotNull(HeaderKeys.Limit, limit.ToString());
            parameters.AddIfNotNull(HeaderKeys.Offset, offset.ToString());
            parameters.AddIfNotNull(HeaderKeys.FilterDue, filterDue.GetEnumMemberValue());
            parameters.AddIfNotNull(HeaderKeys.FilterComplete, filterComplete.GetEnumMemberValue());
        }

        var paramString = parameters.ToString();
        var queryString = string.IsNullOrWhiteSpace(paramString) ?
            string.Format(ApiEndpoints.GetAllTasks, apiVersion) : $"{string.Format(ApiEndpoints.GetAllTasks, apiVersion)}&{parameters}";
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
    /// Gets a task by its identifier.
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="eventId">The event identifier.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameters are invalid.</exception>
    public async Task<Result<EventResult, CommonErrorCode>?> GetTaskById(
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
            string.Format(ApiEndpoints.GetTaskById, apiVersion) : $"{string.Format(ApiEndpoints.GetTaskById, apiVersion)}&{parameters}";
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
    /// Sets some task data.
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="calendarId">The calendar identifier.</param>
    /// <param name="originalCalendarId">The original calendar identifier.</param>
    /// <param name="summary">The summary.</param>
    /// <param name="timeZoneId">The time zone identifier.</param>
    /// <param name="notifications">The notifications.</param>
    /// <param name="percentageComplete">The percentage complete.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameters are invalid.</exception>
    public async Task<Result<EventResult, CommonErrorCode>?> SetTaskData(
        int apiVersion,
        string calendarId,
        string originalCalendarId,
        string summary,
        string? timeZoneId = null,
        List<EventNotification>? notifications = null,
        uint? percentageComplete = null)
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
            parameters.AddIfNotNull(HeaderKeys.OriginalCalendarId, originalCalendarId);
            parameters.AddIfNotNull(HeaderKeys.Summary, summary);
            parameters.AddIfNotNull(HeaderKeys.TimezoneId, timeZoneId);
            parameters.AddIfNotNull(HeaderKeys.EventNotifications, notifications);
            parameters.AddIfNotNull(HeaderKeys.PercentComplete, percentageComplete?.ToString());
        }

        var paramString = parameters.ToString();
        var queryString = string.IsNullOrWhiteSpace(paramString) ?
            string.Format(ApiEndpoints.SetTaskData, apiVersion) : $"{string.Format(ApiEndpoints.SetTaskData, apiVersion)}&{parameters}";
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
    /// Deletes a task by its identifier.
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="eventId">The event identifier.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameters are invalid.</exception>
    public async Task<Result<EventResult, CommonErrorCode>?> DeleteTaskById(
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
            string.Format(ApiEndpoints.DeleteTaskById, apiVersion) : $"{string.Format(ApiEndpoints.DeleteTaskById, apiVersion)}&{parameters}";
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

    /// <summary>
    /// Cleans all completed tasks.
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="calendarIdList">The calendar identifier list.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameters are invalid.</exception>
    public async Task<Result<EventResult, CommonErrorCode>?> CleanCompletedTasks(
        int apiVersion,
        List<string> calendarIdList)
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
        }

        var paramString = parameters.ToString();
        var queryString = string.IsNullOrWhiteSpace(paramString) ?
            string.Format(ApiEndpoints.CleanCompletedTasks, apiVersion) : $"{string.Format(ApiEndpoints.CleanCompletedTasks, apiVersion)}&{parameters}";
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
