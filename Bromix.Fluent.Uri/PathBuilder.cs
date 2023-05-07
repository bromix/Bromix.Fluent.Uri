namespace Bromix.Fluent.Uri;

/// <summary>
/// A utility class for building URL paths with proper escaping.
/// </summary>
public sealed class PathBuilder
{
    private readonly List<string> _pathComponents = new();

    /// <summary>
    /// Sets the path of the builder to the specified value, splitting it into components and escaping them.
    /// </summary>
    /// <param name="pathComponent"></param>
    /// <param name="pathComponents"></param>
    public PathBuilder SetPath(string pathComponent, params string[] pathComponents)
    {
        InternalAddPathComponents(new[] { pathComponent }.Concat(pathComponents), true);
        return this;
    }

    /// <summary>
    /// Adds one or more path components to the builder, escaping them.
    /// </summary>
    /// <param name="pathComponent">The first path component to add.</param>
    /// <param name="pathComponents">Additional path components to add.</param>
    public PathBuilder AddPath(string pathComponent, params string[] pathComponents)
    {
        InternalAddPathComponents(new[] { pathComponent }.Concat(pathComponents), false);
        return this;
    }

    /// <summary>
    /// Adds a sequence of path components to the builder, escaping them.
    /// </summary>
    /// <param name="pathComponents">The path components to add.</param>
    /// <param name="replace"></param>
    private void InternalAddPathComponents(IEnumerable<string> pathComponents, bool replace)
    {
        if (replace)
        {
            _pathComponents.Clear();
        }

        foreach (var pathComponent in pathComponents)
        {
            var parts = pathComponent
                .Split('/')
                .Where(path => !string.IsNullOrEmpty(path))
                .Select(System.Uri.EscapeDataString);
            _pathComponents.AddRange(parts);
        }
    }

    /// <summary>
    /// Gets the current path as a single string, with each component separated by slashes.
    /// </summary>
    public string Path => string.Join('/', _pathComponents);
}