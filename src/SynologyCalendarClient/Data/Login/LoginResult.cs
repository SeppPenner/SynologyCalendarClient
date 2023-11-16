// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginResult.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//  The login result data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Data.Login;

/// <summary>
/// The login result data.
/// </summary>
public sealed record class LoginResult
{
    /// <summary>
    /// Gets or sets the SID.
    /// </summary>
    [JsonProperty("sid")]
    public string Sid { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the syno token.
    /// </summary>
    [JsonProperty("synotoken")]
    public string SynoToken { get; init; } = string.Empty;
}
