// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterComplete.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//  The complete time filter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Data.Task;

/// <summary>
/// The complete time filter.
/// </summary>
public enum FilterComplete
{
    /// <summary>
    /// Only shows completed tasks.
    /// </summary>
    [EnumMember(Value = "1")]
    OnlyShowCompletedTasks = 1,

    /// <summary>
    /// Only shows not completed tasks.
    /// </summary>
    [EnumMember(Value = "2")]
    OnlyShowNotCompletedTasks = 2,

    /// <summary>
    /// Show both.
    /// </summary>
    [EnumMember(Value = "3")]
    ShowBoth = 3
}
