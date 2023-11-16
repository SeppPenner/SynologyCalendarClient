// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventNotification.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//  The event notification data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Data.Event;

/// <summary>
/// The event notification data.
/// </summary>
public sealed record class EventNotification
{
    /// <summary>
    /// Gets or sets the time value.
    /// </summary>
    [JsonProperty("time_value")]
    public ulong TimeValue { get; init; }

    /// <summary>
    /// Gets or sets the time format.
    /// </summary>
    [JsonProperty("time_format")]
    public ulong TimeFormat { get; init; }

    /// <summary>
    /// Gets or sets the alarm action.
    /// </summary>
    [JsonProperty("alarm_action")]
    public ulong AlarmAction { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the alarm ICAL is set or not.
    /// </summary>
    [JsonProperty("alarm_ical")]
    public bool AlarmIcal { get; init; }
}
