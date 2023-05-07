using System.Collections.Specialized;

namespace Bromix.Fluent.Uri;

internal sealed class RelativeUriBuilder : IRelativeUriBuilder
{
    internal RelativeUriBuilder()
    {
    }

    internal RelativeUriBuilder(string relativePath)
    {
        _pathBuilder.AddPath(relativePath);
    }

    public IRelativeUriBuilder WithQuery(Action<QueryBuilder> builder)
    {
        builder.Invoke(_queryBuilder);
        return this;
    }

    public IRelativeUriBuilder AddQueryFrom(NameValueCollection values)
    {
        _queryBuilder.Add(values);
        return this;
    }

    public IRelativeUriBuilder AddQuery(string name, string value)
    {
        _queryBuilder.Add(name, value);
        return this;
    }

    public IRelativeUriBuilder AddPath(string pathComponent, params string[] pathComponents)
    {
        _pathBuilder.AddPath(pathComponent, pathComponents);
        return this;
    }

    public IRelativeUriBuilder WithFragment(string fragment)
    {
        _uriBuilder.Fragment = System.Uri.EscapeDataString(fragment);
        return this;
    }

    public System.Uri Uri
    {
        get
        {
            _uriBuilder.Path = _pathBuilder.Path;
            _uriBuilder.Query = _queryBuilder.Query;
            var uri = _uriBuilder.Uri;
            return new System.Uri(uri.PathAndQuery + uri.Fragment, UriKind.Relative);
        }
    }

    private readonly UriBuilder _uriBuilder = new();
    private readonly PathBuilder _pathBuilder = new();
    private readonly QueryBuilder _queryBuilder = new();
}