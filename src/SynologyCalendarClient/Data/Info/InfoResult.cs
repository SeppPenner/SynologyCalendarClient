// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfoResult.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//  The info result data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Data.Info;

/// <summary>
/// The info result data.
/// </summary>
public sealed record class InfoResult
{
    /// <summary>
    /// Gets or sets the path.
    /// </summary>
    [JsonProperty("path")]
    public string Path { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the minimum version.
    /// </summary>
    [JsonProperty("minVersion")]
    public int MinimumVersion { get; init; }

    /// <summary>
    /// Gets or sets the maximum version.
    /// </summary>
    [JsonProperty("maxVersion")]
    public int MaximumVersion { get; init; }
}
