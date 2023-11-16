// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryParameters.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//  A collection to handle query parameters.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient;

/// <summary>
/// A collection to handle query parameters.
/// </summary>
public sealed class QueryParameters
{
    /// <summary>
    /// The parameters.
    /// </summary>
    private readonly Dictionary<string, string> parameters = new ();

    /// <summary>
    /// Adds the given key and value to the query parameters if it is not null or whitespace.
    /// </summary>
    /// <typeparam name="T">The type parameter.</typeparam>
    /// <param name="parameterKey">The parameter key.</param>
    /// <param name="parameterValue">The parameter value.</param>
    /// <exception cref="InvalidOperationException">Thrown if the key already exists.</exception>
    public void AddIfNotNull<T>(string parameterKey, List<T>? parameterValue)
    {
        if (this.parameters.ContainsKey(parameterKey))
        {
            throw new InvalidOperationException(string.Format("The key {0} already exists.", parameterKey));
        }

        if (parameterValue.IsEmptyOrNull())
        {
            return;
        }

        this.parameters.Add(parameterKey, JsonConvert.SerializeObject(parameterValue));
    }

    /// <summary>
    /// Adds the given key and value to the query parameters if it is not null or whitespace.
    /// </summary>
    /// <param name="parameterKey">The parameter key.</param>
    /// <param name="parameterValue">The parameter value.</param>
    /// <exception cref="InvalidOperationException">Thrown if the key already exists.</exception>
    public void AddIfNotNull(string parameterKey, string? parameterValue)
    {
        if (this.parameters.ContainsKey(parameterKey))
        {
            throw new InvalidOperationException(string.Format("The key {0} already exists.", parameterKey));
        }

        if (string.IsNullOrWhiteSpace(parameterValue))
        {
            return;
        }

        switch (parameterValue)
        {
            case "True":
                this.parameters.Add(parameterKey, "true");
                break;
            case "False":
                this.parameters.Add(parameterKey, "false");
                break;
            default:
                this.parameters.Add(parameterKey, parameterValue);
                break;
        }
    }

    /// <inheritdoc cref="object"/>
    public override string ToString()
    {
        var sb = new StringBuilder();

        foreach (var kvp in this.parameters)
        {
            if (sb.Length > 0)
            {
                sb.Append('&');
            }

            sb.AppendFormat("{0}={1}",
                HttpUtility.UrlEncode(kvp.Key),
                HttpUtility.UrlEncode(kvp.Value));
        }

        return sb.ToString();
    }
}
