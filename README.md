SynologyCalendarClient
====================================

SynologyCalendarClient is a project to handle Synology calendar API requests. However, as the Synology calendar API description is very bad, it is not yet finished.

[![Build status](https://ci.appveyor.com/api/projects/status/g97r2ruunhxqphtu?svg=true)](https://ci.appveyor.com/project/SeppPenner/SynologyCalendarClient)
[![GitHub issues](https://img.shields.io/github/issues/SeppPenner/SynologyCalendarClient.svg)](https://github.com/SeppPenner/SynologyCalendarClient/issues)
[![GitHub forks](https://img.shields.io/github/forks/SeppPenner/SynologyCalendarClient.svg)](https://github.com/SeppPenner/SynologyCalendarClient/network)
[![GitHub stars](https://img.shields.io/github/stars/SeppPenner/SynologyCalendarClient.svg)](https://github.com/SeppPenner/SynologyCalendarClient/stargazers)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://raw.githubusercontent.com/SeppPenner/SynologyCalendarClient/master/License.txt)
[![Known Vulnerabilities](https://snyk.io/test/github/SeppPenner/SynologyCalendarClient/badge.svg)](https://snyk.io/test/github/SeppPenner/SynologyCalendarClient)

## Available for
* Net 6.0
* Net 7.0
* Net 8.0

## Net Core and Net Framework latest and LTS versions
* https://dotnet.microsoft.com/download/dotnet

## Basic usage
```csharp
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
```

## Issues
* Creating a event still fails: https://stackoverflow.com/questions/77349742/how-to-use-the-synology-calendar-api-to-create-events-in-c
* Tested methods: Login, Logout, Info, GetAllCalendars, GetCalendarById.
* Missing deserialization tests for Events and Tasks.
* The API description is pretty bad, so I decided to use CalDav. Checkout https://github.com/SeppPenner/CalDAVNet and https://github.com/SeppPenner/CalDavSynologySyncer for examples.

Change history
--------------

See the [Changelog](https://github.com/SeppPenner/SynologyCalendarClient/blob/master/Changelog.md).
