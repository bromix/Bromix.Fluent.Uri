using System.Collections.Specialized;

namespace Bromix.Fluent.Uri;

/// <summary>
/// Defines a set of methods for building a URI with different components.
/// </summary>
public interface IUriBuilder
{
    /// <summary>
    /// Sets the username for the URI.
    /// </summary>
    /// <param name="userName">The username to be set.</param>
    /// <returns>The current instance of the <see cref="IUriBuilder"/> interface.</returns>
    IUriBuilder WithUserName(string userName);

    /// <summary>
    /// Sets the username and password for the URI.
    /// </summary>
    /// <param name="userName">The username to be set.</param>
    /// <param name="password">The password to be set.</param>
    /// <returns>The current instance of the <see cref="IUriBuilder"/> interface.</returns>
    IUriBuilder WithUserName(string userName, string password);

    /// <summary>
    /// Sets the port number for the URI.
    /// </summary>
    /// <param name="port">The port number to be set.</param>
    /// <returns>The current instance of the <see cref="IUriBuilder"/> interface.</returns>
    IUriBuilder WithPort(int port);

    /// <summary>
    /// Configures the URI's query parameters.
    /// </summary>
    /// <param name="builder">An <see cref="Action{T}"/> that takes a <see cref="QueryBuilder"/> instance as a parameter and applies query parameter modifications.</param>
    /// <returns>The current instance of the <see cref="IUriBuilder"/> interface.</returns>
    IUriBuilder WithQuery(Action<QueryBuilder> builder);


    /// <summary>
    /// Adds a single query parameter to the URI.
    /// </summary>
    /// <param name="name">The name of the query parameter.</param>
    /// <param name="value">The value of the query parameter.</param>
    /// <returns>The current instance of the <see cref="IUriBuilder"/> interface.</returns>
    IUriBuilder AddQuery(string name, string value);

    IUriBuilder AddQueryFrom(NameValueCollection values);

    /// <summary>
    /// Adds one or more path components to the URI.
    /// </summary>
    /// <param name="pathComponent">The first path component to be added.</param>
    /// <param name="pathComponents">Additional path components to be added.</param>
    /// <returns>The current instance of the <see cref="IUriBuilder"/> interface.</returns>
    IUriBuilder AddPath(string pathComponent, params string[] pathComponents);

    /// <summary>
    /// Sets the fragment for the URI.
    /// </summary>
    /// <param name="fragment">The fragment to be set.</param>
    /// <returns>The current instance of the <see cref="IUriBuilder"/> interface.</returns>
    IUriBuilder WithFragment(string fragment);

    /// <summary>
    /// Gets the URI that has been built by the builder.
    /// </summary>
    System.Uri Uri { get; }
}