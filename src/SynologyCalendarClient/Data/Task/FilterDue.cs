// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterDue.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//  The due time filter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Data.Task;

/// <summary>
/// The due time filter.
/// </summary>
public enum FilterDue
{
    /// <summary>
    /// Only shows tasks with due time.
    /// </summary>
    [EnumMember(Value = "1")]
    OnlyShowTasksWithDueTime = 1,

    /// <summary>
    /// Only shows tasks without due time.
    /// </summary>
    [EnumMember(Value = "2")]
    OnlyShowTasksWithoutDueTime = 2,

    /// <summary>
    /// Show both.
    /// </summary>
    [EnumMember(Value = "3")]
    ShowBoth = 3
}
