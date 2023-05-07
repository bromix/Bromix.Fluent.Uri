using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace Bromix.Fluent.Uri;

/// <summary>
/// A utility class for building URL query strings with proper encoding.
/// </summary>
public sealed class QueryBuilder
{
    private readonly NameValueCollection _nameValueCollection = new();
    private bool _changed;
    private string _query = string.Empty;

    /// <summary>
    /// Adds a name-value pair to the query string.
    /// </summary>
    /// <param name="name">The name of the parameter to add.</param>
    /// <param name="value">The value of the parameter to add.</param>
    /// <returns>The QueryBuilder instance, to enable method chaining.</returns>
    public QueryBuilder Add(string name, string value)
    {
        _nameValueCollection.Add(name, value);
        _changed = true;
        return this;
    }

    /// <summary>
    /// Adds a collection of name-value pairs to the query string.
    /// </summary>
    /// <param name="values">The collection of parameters to add.</param>
    /// <returns>The QueryBuilder instance, to enable method chaining.</returns>
    public QueryBuilder Add(NameValueCollection values)
    {
        _nameValueCollection.Add(values);
        _changed = true;
        return this;
    }

    /// <summary>
    /// Gets the current query string.
    /// </summary>
    public string Query => ToString();

    /// <summary>
    /// Returns the current query string, encoding all parameters properly.
    /// </summary>
    /// <returns>The encoded query string.</returns>
    public override string ToString()
    {
        if (!_changed)
        {
            return _query;
        }

        var sb = new StringBuilder();
        var appendAmpersand = false;

        foreach (var name in _nameValueCollection.AllKeys)
        {
            // omit empty keys.
            if (string.IsNullOrWhiteSpace(name))
            {
                continue;
            }

            var values = _nameValueCollection.GetValues(name);

            // omit empty values.
            if (values is null || values.Length == 0)
            {
                continue;
            }

            // There are two options for multiple values with the same name:
            // 1. Separated by comma.
            // 2. Multiple name=value pairs.

            foreach (var value in values)
            {
                if (appendAmpersand)
                {
                    sb.Append('&');
                }

                sb.Append($"{HttpUtility.UrlEncode(name)}={HttpUtility.UrlEncode(value)}");

                appendAmpersand = true;
            }
        }

        _query = sb.ToString();
        _changed = false;
        return _query;
    }
}
