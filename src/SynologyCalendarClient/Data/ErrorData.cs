// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorData.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//  The error data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Data;

/// <summary>
/// The error data.
/// </summary>
public sealed record class ErrorData<T>
    where T: struct
{
    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    [JsonProperty("code")]
    public T ErrorCode { get; init; }
}
