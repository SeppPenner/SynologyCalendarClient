// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarResult.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The calendar result data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Data.Calendar;

/// <summary>
/// The calendar result data.
/// </summary>
public sealed record class CalendarResult
{
    /// <summary>
    /// Gets or sets the calendar color.
    /// </summary>
    [JsonProperty("cal_color")]
    public string CalendarColor { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the calendar description.
    /// </summary>
    [JsonProperty("cal_description")]
    public string CalendarDescription { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the calendar display name.
    /// </summary>
    [JsonProperty("cal_displayname")]
    public string CalendarDisplayName { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the calendar extra information.
    /// </summary>
    [JsonProperty("cal_extra_info")]
    public string CalendarExtraInformation { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the calendar identifier.
    /// </summary>
    [JsonProperty("cal_id")]
    public string CalendarId { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the privilege.
    /// </summary>
    [JsonProperty("privilege")]
    public string Privilege { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the public sharing identifier.
    /// </summary>
    [JsonProperty("cal_public_sharing_id")]
    public string PublicSharingId { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the create time.
    /// </summary>
    /// </summary>
    [JsonProperty("create_time")]
    public string CreateTime { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the is event flag.
    /// </summary>
    [JsonProperty("is_evt")]
    public bool IsEvent { get; init; }

    /// <summary>
    /// Gets or sets the is hidden in calendar flag.
    /// </summary>
    [JsonProperty("is_hidden_in_cal")]
    public bool IsHiddenInCalendar { get; init; }

    /// <summary>
    /// Gets or sets the is hidden in list flag.
    /// </summary>
    [JsonProperty("is_hidden_in_list")]
    public bool IsHiddenInList { get; init; }

    /// <summary>
    /// Gets or sets the modify time.
    /// </summary>
    [JsonProperty("modify_time")]
    public string ModifyTime { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the notify alarm by browser flag.
    /// </summary>
    [JsonProperty("notify_alarm_by_browser")]
    public bool NotifyAlarmByBrowser { get; init; }

    /// <summary>
    /// Gets or sets the notify alarm by mail flag.
    /// </summary>
    [JsonProperty("notify_alarm_by_mail")]
    public bool NotifyAlarmByMail { get; init; }

    /// <summary>
    /// Gets or sets the notify daily agenda.
    /// </summary>
    [JsonProperty("notify_daily_agenda")]
    public string NotifyDailyAgenda { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the notify alarm by browser flag.
    /// </summary>
    [JsonProperty("notify_evt_by_browser")]
    public bool NotifyEventByBrowser { get; init; }

    /// <summary>
    /// Gets or sets the notify alarm by mail flag.
    /// </summary>
    [JsonProperty("notify_evt_by_mail")]
    public bool NotifyEventByMail { get; init; }

    /// <summary>
    /// Gets or sets the notify event create.
    /// </summary>
    [JsonProperty("notify_evt_create")]
    public string NotifyEventCreate { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the notify event delete.
    /// </summary>
    [JsonProperty("notify_evt_delete")]
    public string NotifyEventDelete { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the notify event RSVP.
    /// </summary>
    [JsonProperty("notify_evt_rsvp")]
    public string NotifyEventRsvp { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the notify event set.
    /// </summary>
    [JsonProperty("notify_evt_set")]
    public string NotifyEventSet { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the notify import calendar by browser flag.
    /// </summary>
    [JsonProperty("notify_import_cal_by_browser")]
    public bool NotifyImportCalendarByBrowser { get; init; }

    /// <summary>
    /// Gets or sets the notify import calendar by mail flag.
    /// </summary>
    [JsonProperty("notify_import_cal_by_mail")]
    public bool NotifyImportCalendarByMail { get; init; }

    /// <summary>
    /// Gets or sets the original calendar identifier.
    /// </summary>
    [JsonProperty("original_cal_id")]
    public string OriginalCalendarId { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the user group name.
    /// </summary>
    [JsonProperty("ug_name")]
    public string UserGroup { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the user number.
    /// </summary>
    [JsonProperty("user_no")]
    public ulong UserNumber { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether this calendar is the default calendar or not.
    /// </summary>
    [JsonProperty("default_calendar")]
    public bool DefaultCalendar { get; init; }

    /// <summary>
    /// Gets or sets the calendar order.
    /// </summary>
    [JsonProperty("cal_order")]
    public string CalendarOrder { get; init; } = string.Empty;
}
