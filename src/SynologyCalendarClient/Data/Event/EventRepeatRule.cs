// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventRepeatRule.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//  The event repeat rule data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Data.Event;

/// <summary>
/// The event repeat rule data.
/// </summary>
public sealed record class EventRepeatRule
{
    /// <summary>
    /// Gets or sets the repeat rule.
    /// </summary>
    [JsonProperty("repeat_rule")]
    public string RepeatRule { get; init; } = string.Empty;
}
