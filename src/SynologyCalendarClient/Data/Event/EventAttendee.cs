// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventAttendee.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The event attendee data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Data.Event;

/// <summary>
/// The event attendee data.
/// </summary>
public sealed record class EventAttendee
{
    /// <summary>
    /// Gets or sets the user group name.
    /// </summary>
    [JsonProperty("ug_name")]
    public string UserGroup { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    [JsonProperty("email")]
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the invite type.
    /// </summary>
    [JsonProperty("invite_type")]
    public uint InviteType { get; init; }

    /// <summary>
    /// Gets or sets the invite actor.
    /// </summary>
    [JsonProperty("invite_actor")]
    public string InviteActor { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the invite status.
    /// </summary>
    [JsonProperty("invite_status")]
    public string InviteStatus { get; init; } = string.Empty;
}
