// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynologyCalendarClient.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The main Synology calendar API client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Client;

/// <summary>
/// The main Synology calendar API client.
/// </summary>
public partial class SynologyCalendarClient
{
    /// <summary>
    /// Does a user login.
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="account">The account.</param>
    /// <param name="password">The password.</param>
    /// <param name="session">The session.</param>
    /// <param name="enableSynoToken">A value indicating whether the Synology token is enabled or not.</param>
    /// <param name="format">The format.</param>
    /// <param name="otpCode">The OTP code.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameters are invalid.</exception>
    public async Task<Result<LoginResult, LoginErrorCode>?> Login(
        int apiVersion,
        string account,
        string password,
        string? session = null,
        bool enableSynoToken = false,
        LoginFormat? format = null,
        string? otpCode = null)
    {
        // Some checks.
        if (apiVersion < 1)
        {
            throw new ArgumentException(nameof(apiVersion), "The API version must be bigger than or equal to 1.");
        }

        if (apiVersion >= 1)
        {
            if (string.IsNullOrWhiteSpace(account))
            {
                throw new ArgumentException(nameof(account), "The account name must not be empty.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(nameof(password), "The account password must not be empty.");
            }
        }

        // Fill the parameters.
        var parameters = new QueryParameters();

        if (apiVersion >= 1)
        {
            parameters.AddIfNotNull(HeaderKeys.Account, account);
            parameters.AddIfNotNull(HeaderKeys.Password, password);
            parameters.AddIfNotNull(HeaderKeys.Session, session);
        }

        if (apiVersion >= 2)
        {
            parameters.AddIfNotNull(HeaderKeys.Format, format?.GetEnumMemberValue());
        }

        if (apiVersion >= 3)
        {
            if (enableSynoToken)
            {
                parameters.AddIfNotNull(HeaderKeys.EnableSynoToken, "yes");
            }

            parameters.AddIfNotNull(HeaderKeys.OtpCode, otpCode);
        }

        var paramString = parameters.ToString();
        var queryString = string.IsNullOrWhiteSpace(paramString) ?
            string.Format(ApiEndpoints.Login, apiVersion) : $"{string.Format(ApiEndpoints.Login, apiVersion)}&{parameters}";
        var response = await this.httpClient.GetAsync(queryString);
        var resultString = await response.Content.ReadAsStringAsync();

        try
        {
            var result = JsonConvert.DeserializeObject<Result<LoginResult, LoginErrorCode>?>(resultString);

            if (format != LoginFormat.Cookie && !string.IsNullOrWhiteSpace(result?.Data?.SynoToken))
            {
                this.SynoToken = result.Data.SynoToken;
            }

            return result;
        }
        catch (Exception ex)
        {
            this.Logger.Error(ex, "An error occured when deserializing the API data.");
            return null;
        }
    }

    /// <summary>
    /// Does a user logout.
    /// </summary>
    /// <param name="apiVersion">The API version.</param>
    /// <param name="session">The session.</param>
    /// <returns>The result data or <c>null</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the API version or the query parameters are invalid.</exception>
    public async Task<Result<LogoutResult, CommonErrorCode>?> Logout(
        int apiVersion,
        string? session = null)
    {
        // Some checks.
        if (apiVersion < 1)
        {
            throw new ArgumentException(nameof(apiVersion), "The API version must be bigger than or equal to 1.");
        }

        // Fill the parameters.
        var parameters = new QueryParameters();

        if (apiVersion >= 1)
        {
            parameters.AddIfNotNull(HeaderKeys.Session, session);
        }

        var paramString = parameters.ToString();
        var queryString = string.IsNullOrWhiteSpace(paramString) ?
            string.Format(ApiEndpoints.Logout, apiVersion) : $"{string.Format(ApiEndpoints.Logout, apiVersion)}&{parameters}";
        var response = await this.httpClient.GetAsync(queryString);
        var resultString = await response.Content.ReadAsStringAsync();

        try
        {
            // Todo: Check, maybe the response is string.Empty?
            return JsonConvert.DeserializeObject<Result<LogoutResult, CommonErrorCode>?>(resultString);
        }
        catch (Exception ex)
        {
            this.Logger.Error(ex, "An error occured when deserializing the API data.");
            return null;
        }
    }
}
