// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginErrorCode.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//  The login error code.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Data.Login;

/// <summary>
/// The login error code.
/// </summary>
public enum LoginErrorCode
{
    /// <summary>
    /// No such account or the password is incorrect.
    /// </summary>
    NoAccountFoundOrPasswordInvalid = 400,

    /// <summary>
    /// Account disabled.
    /// </summary>
    AccountDisabled = 401,

    /// <summary>
    /// Permission denied.
    /// </summary>
    PermissionDenied = 402,

    /// <summary>
    /// 2-step verification code required.
    /// </summary>
    TwoFactorCodeRequired = 403,

    /// <summary>
    /// Failed to authenticate 2-step verification code.
    /// </summary>
    TwoFactorCodeInvalid = 404
}
