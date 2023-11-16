// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//  The enum extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Extensions;

/// <summary>
/// The enum extensions.
/// </summary>
internal static class EnumExtensions
{
    /// <summary>
    /// Gets the name from the EnumMember attribute.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns>The <see cref="string"/> value of the <see cref="EnumMemberAttribute"/>.</returns>
    public static string? GetEnumMemberValue<T>(this T? value) where T : Enum
    {
        return typeof(T)
            .GetTypeInfo()
            .DeclaredMembers
            .SingleOrDefault(x => x.Name == value?.ToString())
            ?.GetCustomAttribute<EnumMemberAttribute>(false)
            ?.Value;
    }
}
