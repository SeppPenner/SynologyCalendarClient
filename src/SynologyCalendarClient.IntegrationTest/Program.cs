// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//  A program to test the Synology calendar client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.IntegrationTest;

/// <summary>
/// A program to test the Synology calendar client.
/// </summary>
public static class Program
{
    /// <summary>
    /// The main method of the program.
    /// </summary>
    public static async Task Main()
    {
        // Create the logger.
        var logger = LoggerConfig.GetLoggerConfiguration(nameof(Program))
            .WriteTo.Sink((ILogEventSink)Log.Logger)
            .CreateLogger();

        // Create the Synology calendar client.
        var synologyCalendarClient = new SynologyClient("https://example.com/calendars", logger);

        // Login.
        var loginResult = await synologyCalendarClient.Login(
                    3,
                    "username",
                    "password",
                    enableSynoToken: true,
                    format: LoginFormat.Sid);

        // If the login failed, jump out.
        if (loginResult is null || !loginResult.Success)
        {
            logger.Error("The Synology login failed");
            return;
        }

        // Get all calendars.
        var calendarsResult = await synologyCalendarClient.GetAllCalendars(
            1,
            true,
            false);

        // If the login failed, jump out.
        if (calendarsResult is null || !calendarsResult.Success)
        {
            logger.Error("Loading calendars failed");
            return;
        }
    }
}
