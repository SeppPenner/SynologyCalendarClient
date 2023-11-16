// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginFormat.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//  The login formats.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Data.Login;

/// <summary>
/// The login formats.
/// </summary>
public enum LoginFormat
{
    /// <summary>
    /// The cookie login format.
    /// </summary>
    [EnumMember(Value = "cookie")]
    Cookie = 0,

    /// <summary>
    /// The cookie login format.
    /// </summary>
    [EnumMember(Value = "sid")]
    Sid = 1
}
