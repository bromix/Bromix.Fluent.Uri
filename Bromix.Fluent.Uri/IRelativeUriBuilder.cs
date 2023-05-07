using System.Collections.Specialized;

namespace Bromix.Fluent.Uri;

/// <summary>
/// Defines a set of methods for building a relative URI with different components.
/// </summary>
public interface IRelativeUriBuilder
{
    /// <summary>
    /// Configures the relative URI's query parameters.
    /// </summary>
    /// <param name="builder">An <see cref="Action{T}"/> that takes a <see cref="QueryBuilder"/> instance as a parameter and applies query parameter modifications.</param>
    /// <returns>The current instance of the <see cref="IRelativeUriBuilder"/> interface.</returns>
    IRelativeUriBuilder WithQuery(Action<QueryBuilder> builder);


    /// <summary>
    /// Adds a single query parameter to the relative URI.
    /// </summary>
    /// <param name="name">The name of the query parameter.</param>
    /// <param name="value">The value of the query parameter.</param>
    /// <returns>The current instance of the <see cref="IRelativeUriBuilder"/> interface.</returns>
    IRelativeUriBuilder AddQuery(string name, string value);

    IRelativeUriBuilder AddQueryFrom(NameValueCollection values);

    /// <summary>
    /// Adds one or more path components to the relative URI.
    /// </summary>
    /// <param name="pathComponent">The first path component to be added.</param>
    /// <param name="pathComponents">Additional path components to be added.</param>
    /// <returns>The current instance of the <see cref="IRelativeUriBuilder"/> interface.</returns>
    IRelativeUriBuilder AddPath(string pathComponent, params string[] pathComponents);

    /// <summary>
    /// Sets the fragment for the relative URI.
    /// </summary>
    /// <param name="fragment">The fragment to be set.</param>
    /// <returns>The current instance of the <see cref="IRelativeUriBuilder"/> interface.</returns>
    IRelativeUriBuilder WithFragment(string fragment);

    /// <summary>
    /// Gets the relative URI that has been built by the builder.
    /// </summary>
    System.Uri Uri { get; }
}