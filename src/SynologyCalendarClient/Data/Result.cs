// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Result.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//  The result data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Data;

/// <summary>
/// The result data.
/// </summary>
public sealed record class Result<TData, TErrorCode>
    where TData : class, new()
    where TErrorCode : struct
{
    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    [JsonProperty("data")]
    public TData? Data { get; init; }

    /// <summary>
    /// Gets or sets a value indicating success or not.
    /// </summary>
    [JsonProperty("success")]
    public bool Success { get; init; }

    /// <summary>
    /// Gets or sets the error data.
    /// </summary>
    [JsonProperty("error")]
    public ErrorData<TErrorCode>? ErrorData { get; init; }
}
