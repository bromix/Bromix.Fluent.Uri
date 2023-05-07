namespace Bromix.Fluent.Uri;

/// <summary>
/// Provides a fluent interface for building URIs.
/// </summary>
public static class FluentUriBuilder
{
    /// <summary>
    /// Creates a new instance of an <see cref="IUriBuilder"/> with the specified scheme and host.
    /// </summary>
    /// <param name="scheme">The URI scheme (e.g. "http", "https").</param>
    /// <param name="host">The URI host (e.g. "example.com", "localhost").</param>
    /// <returns>A new instance of an <see cref="IUriBuilder"/>.</returns>
    public static IUriBuilder Create(string scheme, string host)
    {
        UriHelper.ValidateScheme(scheme);
        UriHelper.ValidateHost(host);

        return new AbsoluteUriBuilder(scheme, host);
    }

    /// <summary>
    /// Creates a new instance of an <see cref="IRelativeUriBuilder"/> without a relative path.
    /// </summary>
    /// <returns>A new instance of an <see cref="IRelativeUriBuilder"/>.</returns>
    public static IRelativeUriBuilder CreateRelative() => new RelativeUriBuilder();

    /// <summary>
    /// Creates a new instance of an <see cref="IRelativeUriBuilder"/> with the specified relative path.
    /// </summary>
    /// <param name="relativePath">The relative path to append to the URI.</param>
    /// <returns>A new instance of an <see cref="IRelativeUriBuilder"/> with the specified relative path.</returns>
    public static IRelativeUriBuilder CreateRelative(string relativePath) => new RelativeUriBuilder(relativePath);
}