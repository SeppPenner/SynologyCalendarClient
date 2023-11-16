// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventResult.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//  The event result data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Data.Event;

/// <summary>
/// The event result data.
/// </summary>
public sealed record class EventResult
{
    /// <summary>
    /// The date format.
    /// </summary>
    private const string dateFormat = "yyyyMMddTHHmmss";

    /// <summary>
    /// Gets or sets the class.
    /// </summary>
    [JsonProperty("class")]
    public string Class { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the create time.
    /// </summary>
    [JsonProperty("create_time")]
    public float CreateTime { get; init; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    [JsonProperty("dtend")]
    public string EndDate { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    [JsonProperty("dtstart")]
    public string StartDate { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the event color.
    /// </summary>
    [JsonProperty("evt_color")]
    public string EventColor { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the event ICAL.
    /// </summary>
    [JsonProperty("evt_ical")]
    public string EventIcal { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the event identifier.
    /// </summary>
    [JsonProperty("evt_id")]
    public string EventId { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the repeat rule.
    /// </summary>
    [JsonProperty("evt_repeat_setting")]
    public EventRepeatRule RepeatRule { get; init; } = new();

    /// <summary>
    /// Gets or sets the from Synology app url.
    /// </summary>
    [JsonProperty("from_syno_app_url")]
    public string FromSynoAppUrl { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the gps.
    /// </summary>
    [JsonProperty("gps")]
    public string Gps { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the ICAL uid identifier.
    /// </summary>
    [JsonProperty("ical_uid")]
    public string IcalUid { get; init; } = string.Empty;

    /// <summary>
    ///  Gets or sets a value indicating whether the event is an all-day event or not.
    /// </summary>
    [JsonProperty("is_all_day")]
    public bool IsAllDay { get; init; }

    /// <summary>
    ///  Gets or sets a value indicating whether the event is a repeat event or not.
    /// </summary>
    [JsonProperty("is_repeat_evt")]
    public bool IsRepeatEvent { get; init; }

    /// <summary>
    /// Gets or sets the location.
    /// </summary>
    [JsonProperty("location")]
    public string Location { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the modify time 2.
    /// </summary>
    [JsonProperty("modify_time2")]
    public float ModifyTime2 { get; init; }

    /// <summary>
    /// Gets or sets the original calendar identifier.
    /// </summary>
    [JsonProperty("original_cal_id")]
    public string OriginalCalendarIdentifier { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the owner.
    /// </summary>
    [JsonProperty("owner")]
    public uint Owner { get; init; }

    /// <summary>
    /// Gets or sets the percent complete value.
    /// </summary>
    [JsonProperty("percent_complete")]
    public uint? PercentComplete { get; init; }

    /// <summary>
    /// Gets or sets the priority.
    /// </summary>
    [JsonProperty("priority")]
    public uint? Priority { get; init; }

    /// <summary>
    /// Gets or sets the priority order.
    /// </summary>
    [JsonProperty("priority_order")]
    public uint PriorityOrder { get; init; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the summary.
    /// </summary>
    [JsonProperty("summary")]
    public string Summary { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the transparent.
    /// </summary>
    [JsonProperty("transp")]
    public string Transparent { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the timezone identifier.
    /// </summary>
    [JsonProperty("tz_id")]
    public string TimezoneId { get; init; } = string.Empty;

    /// <summary>
    /// Gets the start date.
    /// </summary>
    [JsonIgnore]
    public DateTimeOffset? DateStart => ParseDate(this.StartDate);

    /// <summary>
    /// Gets the end date.
    /// </summary>
    [JsonIgnore]
    public DateTimeOffset? DateEnd => ParseDate(this.EndDate);

    /// <summary>
    /// Parses the date.
    /// </summary>
    /// <param name="dateString">The date string.</param>
    /// <returns>The parsed <see cref="DateTimeOffset"/>.</returns>
    private static DateTimeOffset? ParseDate(string dateString)
    {
        var splitData = dateString.Split(":");
        
        if (splitData.Length != 2)
        {
            return null;
        }

        return DateTime.ParseExact(splitData[1], dateFormat, CultureInfo.InvariantCulture);
    }
}
