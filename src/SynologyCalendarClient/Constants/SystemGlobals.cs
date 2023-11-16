// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemGlobals.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   Global system settings and functions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Constants;

/// <summary>
/// Global system settings and functions.
/// </summary>
public static class SystemGlobals
{
    /// <summary>
    /// The base unit for the byte conversion.
    /// </summary>
    private const int BaseUnit = 1024;

    /// <summary>
    /// The size units.
    /// </summary>
    private static readonly string[] SizeUnits = { "bytes", "kB", "MB", "GB", "TB" };

    /// <summary>
    /// Gets the data bytes as human readable string.
    /// </summary>
    /// <param name="dataLengthInBytes">The data length in bytes.</param>
    /// <returns>The data bytes as human readable string.</returns>
    public static string GetHumanReadableBytes(string dataLengthInBytes)
    {
        if (ulong.TryParse(dataLengthInBytes, out var value))
        {
            return GetHumanReadableBytes(value);
        }

        return "0 bytes";
    }

    /// <summary>
    /// Gets the data bytes as human readable string.
    /// </summary>
    /// <param name="dataLengthInBytes">The data length in bytes.</param>
    /// <returns>The data bytes as human readable string.</returns>
    public static string GetHumanReadableBytes(int dataLengthInBytes)
    {
        return GetHumanReadableBytes((ulong)dataLengthInBytes);
    }

    /// <summary>
    /// Gets the data bytes as human readable string.
    /// </summary>
    /// <param name="dataLengthInBytes">The data length in bytes.</param>
    /// <returns>The data bytes as human readable string.</returns>
    public static string GetHumanReadableBytes(long dataLengthInBytes)
    {
        return GetHumanReadableBytes((ulong)dataLengthInBytes);
    }

    /// <summary>
    /// Gets the data bytes as human readable string.
    /// </summary>
    /// <param name="dataLengthInBytes">The data length in bytes.</param>
    /// <returns>The data bytes as human readable string.</returns>
    public static string GetHumanReadableBytes(ulong dataLengthInBytes)
    {
        if (dataLengthInBytes == 0)
        {
            return "0 bytes";
        }

        var exponent = (int)(DecimalMath.Log(dataLengthInBytes) / DecimalMath.Log(BaseUnit));
        var convertedSize = dataLengthInBytes / DecimalMath.Power(BaseUnit, exponent);
        return $"{convertedSize} {SizeUnits[exponent]}";
    }
}
