// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiEndpoints.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//  A class containing the API endpoints.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Constants;

/// <summary>
/// A class containing the API endpoints.
/// </summary>
public static class ApiEndpoints
{
    /// <summary>
    /// Gets the API information.
    /// </summary>
    public const string GetApiInformation = "/webapi/query.cgi?api=SYNO.API.Info&version={0}&method=query";

    /// <summary>
    /// Does the login.
    /// </summary>
    public const string Login = "/webapi/auth.cgi?api=SYNO.API.Auth&version={0}&method=login";

    /// <summary>
    /// Does the logout.
    /// </summary>
    public const string Logout = "/webapi/auth.cgi?api=SYNO.API.Auth&version={0}&method=logout";

    /// <summary>
    /// Creates the calendar.
    /// </summary>
    public const string CreateCalendar = "/webapi/entry.cgi?api=SYNO.Cal.Cal&version={0}&method=create";

    /// <summary>
    /// Gets all calendars.
    /// </summary>
    public const string GetAllCalendars = "/webapi/entry.cgi?api=SYNO.Cal.Cal&version={0}&method=list";

    /// <summary>
    /// Gets a calendar by its identifier.
    /// </summary>
    public const string GetCalendarById = "/webapi/entry.cgi?api=SYNO.Cal.Cal&version={0}&method=get";

    /// <summary>
    /// Sets the calendar data.
    /// </summary>
    public const string SetCalendarData = "/webapi/entry.cgi?api=SYNO.Cal.Cal&version={0}&method=set";

    /// <summary>
    /// Deletes a calendar by its identifier.
    /// </summary>
    public const string DeleteCalendarById = "/webapi/entry.cgi?api=SYNO.Cal.Cal&version={0}&method=delete";

    /// <summary>
    /// Creates the event.
    /// </summary>
    public const string CreateEvent = "/webapi/entry.cgi?api=SYNO.Cal.Event&version={0}&method=create";

    /// <summary>
    /// Gets all events.
    /// </summary>
    public const string GetAllEvents = "/webapi/entry.cgi?api=SYNO.Cal.Event&version={0}&method=list";

    /// <summary>
    /// Gets a event by its identifier.
    /// </summary>
    public const string GetEventById = "/webapi/entry.cgi?api=SYNO.Cal.Event&version={0}&method=get";

    /// <summary>
    /// Sets the event data.
    /// </summary>
    public const string SetEventData = "/webapi/entry.cgi?api=SYNO.Cal.Event&version={0}&method=set";

    /// <summary>
    /// Deletes a event by its identifier.
    /// </summary>
    public const string DeleteEventById = "/webapi/entry.cgi?api=SYNO.Cal.Event&version={0}&method=delete";

    /// <summary>
    /// Creates the task.
    /// </summary>
    public const string CreateTask = "/webapi/entry.cgi?api=SYNO.Cal.Todo&version={0}&method=create";

    /// <summary>
    /// Gets all tasks.
    /// </summary>
    public const string GetAllTasks = "/webapi/entry.cgi?api=SYNO.Cal.Todo&version={0}&method=list";

    /// <summary>
    /// Gets a task by its identifier.
    /// </summary>
    public const string GetTaskById = "/webapi/entry.cgi?api=SYNO.Cal.Todo&version={0}&method=get";

    /// <summary>
    /// Sets the task data.
    /// </summary>
    public const string SetTaskData = "/webapi/entry.cgi?api=SYNO.Cal.Todo&version={0}&method=set";

    /// <summary>
    /// Deletes a task by its identifier.
    /// </summary>
    public const string DeleteTaskById = "/webapi/entry.cgi?api=SYNO.Cal.Todo&version={0}&method=delete";

    /// <summary>
    /// Cleans the completed tasks.
    /// </summary>
    public const string CleanCompletedTasks = "/webapi/entry.cgi?api=SYNO.Cal.Todo&version={0}&method=clean_complete";
}
