using System.Text.RegularExpressions;

namespace Bromix.Fluent.Uri;

internal static class UriHelper
{
    /// <summary>
    /// Validates the given port number according to RFC 3986.
    /// </summary>
    /// <param name="port">The port number to validate.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the port number is not within the valid range of 0 to 65535.</exception>
    public static void ValidatePort(int port)
    {
        if (port is < 0 or > 65535)
        {
            throw new ArgumentOutOfRangeException(nameof(port), port, "Port number must be between 0 and 65535");
        }
    }

    /// <summary>
    /// Validates the specified URI scheme according to RFC 3986.
    /// </summary>
    /// <param name="scheme">The URI scheme to validate.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if the scheme is null or empty, does not start with a letter, or contains
    /// any characters other than letters, digits, plus, period, or hyphen.
    /// </exception>
    public static void ValidateScheme(string scheme)
    {
        if (string.IsNullOrEmpty(scheme))
        {
            throw new ArgumentException("Scheme cannot be null or empty", nameof(scheme));
        }

        // According to RFC 3986, the scheme must start with a letter and
        // may consist of letters, digits, plus, period, or hyphen characters.
        if (!char.IsLetter(scheme[0]))
        {
            throw new ArgumentException($"Scheme must start with a letter, found '{scheme[0]}'", nameof(scheme));
        }

        if (!char.IsLetterOrDigit(scheme[^1]))
        {
            throw new ArgumentException($"Scheme must end with a letter or digit, found '{scheme[^1]}'",
                nameof(scheme));
        }

        var schemeLength = scheme.Length;
        for (var i = 1; i < schemeLength - 1; i++)
        {
            var c = scheme[i];

            if (!char.IsLetterOrDigit(c) && c != '+' && c != '.' && c != '-')
            {
                throw new ArgumentException($"Scheme contains invalid character '{c}'", nameof(scheme));
            }
        }
    }


    /// <summary>
    /// Validates a host string based on RFC 3986.
    /// </summary>
    /// <param name="host">The host string to validate.</param>
    /// <exception cref="ArgumentException">
    ///     <paramref name="host"/> is null or empty, or it is not a valid IP address or DNS name.
    ///     The exception message provides more details about the validation failure.
    /// </exception>
    public static void ValidateHost(string host)
    {
        if (string.IsNullOrEmpty(host))
        {
            throw new ArgumentException("Host must not be null or empty.", nameof(host));
        }

        if (IsIPv4Pattern(host))
        {
            ValidateIPv4Pattern(host);
            return;
        }

        // TODO: IPv6 Pattern

        ValidateDnsPattern(host);
    }

    private static void ValidateDnsPattern(string host)
    {
        // Check if the host is a valid DNS name
        // A valid DNS name must consist of one or more labels, separated by periods
        // Each label must consist of letters, digits, or hyphens, and must not start or end with a hyphen
        var labels = host.Split('.');
        foreach (var label in labels)
        {
            if (label.Length is < 1 or > 63)
            {
                throw new ArgumentException($"Invalid length of DNS name format '{label}'.", nameof(host));
            }

            if (label[0] == '-' || label[^1] == '-')
            {
                throw new ArgumentException($"Invalid character '-' in DNS name format '{label}'.",
                    nameof(host));
            }

            foreach (var c in label.Where(c => !char.IsLetterOrDigit(c) && c != '-'))
            {
                throw new ArgumentException($"Invalid character '{c}' in DNS name format '{label}'.",
                    nameof(host));
            }
        }
    }

    private static void ValidateIPv4Pattern(string host)
    {
        var parts = host.Split('.');

        // Each part of an IPv4 address must be an integer between 0 and 255
        foreach (var part in parts)
        {
            if (!int.TryParse(part, out var value) || value < 0 || value > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(host), part,
                    "Invalid IPv4 address format. The value must be an integer between 0 and 255.");
            }
        }
    }


    private static bool IsIPv4Pattern(string host)
    {
        return Regex.Match(host, @"\b(?:\d{1,3}\.){3}\d{1,3}\b").Success;
    }
}