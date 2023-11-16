// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderKeys.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//  The header keys.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Data;

/// <summary>
/// The header keys.
/// </summary>
public static class HeaderKeys
{
    /// <summary>
    /// The query header key.
    /// </summary>
    public const string Query = "query";

    /// <summary>
    /// The session header key.
    /// </summary>
    public const string Session = "session";

    /// <summary>
    /// The account header key.
    /// </summary>
    public const string Account = "account";

    /// <summary>
    /// The password header key.
    /// </summary>
    public const string Password = "passwd";

    /// <summary>
    /// The enable syno token header key.
    /// </summary>
    public const string EnableSynoToken = "enable_syno_token";

    /// <summary>
    /// The format header key.
    /// </summary>
    public const string Format = "format";

    /// <summary>
    /// The OTP code header key.
    /// </summary>
    public const string OtpCode = "otp_code";

    /// <summary>
    /// The display name header key.
    /// </summary>
    public const string CalendarDisplayName = "cal_displayname";

    /// <summary>
    /// Gets the calendar description header key.
    /// </summary>
    public const string CalendarDescription = "cal_description";

    /// <summary>
    /// Gets the calendar color header key.
    /// </summary>
    public const string CalendarColor = "cal_color";

    /// <summary>
    /// Gets the hidden in calendar header key.
    /// </summary>
    public const string IsHiddenInCalendar = "is_hidden_in_cal";

    /// <summary>
    /// Gets the hidden in calendar list header key.
    /// </summary>
    public const string IsHiddenInList = "is_hidden_in_list";

    /// <summary>
    /// Gets the is event header key.
    /// </summary>
    public const string IsEvent = "is_evt";

    /// <summary>
    /// Gets the is todo header key.
    /// </summary>
    public const string IsTodo = "is_todo";

    /// <summary>
    /// Gets the calendar identifier header key.
    /// </summary>
    public const string CalendarId = "cal_id";

    /// <summary>
    /// Gets the calendar extra information.
    /// </summary>
    public const string CalendarExtraInformation = "cal_extra_info";

    /// <summary>
    /// Gets the description header key.
    /// </summary>
    public const string Description = "description";

    /// <summary>
    /// Gets the end date header key.
    /// </summary>
    public const string EndDate = "dtend";

    /// <summary>
    /// Gets the start date header key.
    /// </summary>
    public const string StartDate = "dtstart";

    /// <summary>
    /// Gets the attendees header key.
    /// </summary>
    public const string EventAttendees = "evt_attendee";

    /// <summary>
    /// Gets the notifications header key.
    /// </summary>
    public const string EventNotifications = "evt_notify_setting";

    /// <summary>
    /// Gets the is all day header key.
    /// </summary>
    public const string IsAllDay = "is_all_day";

    /// <summary>
    /// Gets the is repeat event header key.
    /// </summary>
    public const string IsRepeatEvent = "is_repeat_evt";

    /// <summary>
    /// Gets the event location header key.
    /// </summary>
    public const string EventLocation = "evt_location";

    /// <summary>
    /// Gets the original calendar identifier header key.
    /// </summary>
    public const string OriginalCalendarId = "original_cal_id";

    /// <summary>
    /// Gets the summary header key.
    /// </summary>
    public const string Summary = "summary";

    /// <summary>
    /// Gets the transparent header key.
    /// </summary>
    public const string Transparent = "transp";

    /// <summary>
    /// Gets the time zone identifier header key.
    /// </summary>
    public const string TimezoneId = "tzid";

    /// <summary>
    /// Gets the calendar identifier list header key.
    /// </summary>
    public const string CalendarIdentifierList = "cal_id_list";

    /// <summary>
    /// Gets the list repeat header key.
    /// </summary>
    public const string ListRepeat = "list_repeat";

    /// <summary>
    /// Gets the event identifier header key.
    /// </summary>
    public const string EventId = "evt_id";

    /// <summary>
    /// Gets the limit header key.
    /// </summary>
    public const string Limit = "limit";

    /// <summary>
    /// Gets the offset header key.
    /// </summary>
    public const string Offset = "offset";

    /// <summary>
    /// Gets the filter due header key.
    /// </summary>
    public const string FilterDue = "filter_due";

    /// <summary>
    /// Gets the filter complete header key.
    /// </summary>
    public const string FilterComplete = "ilter_complete";

    /// <summary>
    /// Gets the percent complete header key.
    /// </summary>
    public const string PercentComplete = "percent_complete";

    /// <summary>
    /// Gets the syno token header key.
    /// </summary>
    public const string SynoToken = "X-SYNO-TOKEN";
}
