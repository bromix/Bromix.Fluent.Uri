using System.Collections.Specialized;

namespace Bromix.Fluent.Uri;

internal sealed class AbsoluteUriBuilder: IUriBuilder
{
    internal AbsoluteUriBuilder(string scheme, string host)
    {
        _uriBuilder = new UriBuilder(scheme, host);
    }
    public IUriBuilder WithUserName(string userName)
    {
        _uriBuilder.UserName = userName;
        return this;
    }

    public IUriBuilder WithUserName(string userName, string password)
    {
        _uriBuilder.UserName = userName;
        _uriBuilder.Password = password;
        return this;
    }

    public IUriBuilder WithPort(int port)
    {
        UriHelper.ValidatePort(port);
        _uriBuilder.Port = port;
        return this;
    }

    public IUriBuilder WithQuery(Action<QueryBuilder> builder)
    {
        builder.Invoke(_queryBuilder);
        return this;
    }

    public IUriBuilder AddQueryFrom(NameValueCollection values)
    {
        _queryBuilder.Add(values);
        return this;
    }

    public IUriBuilder AddQuery(string name, string value)
    {
        _queryBuilder.Add(name, value);
        return this;
    }

    public IUriBuilder AddPath(string pathComponent, params string[] pathComponents)
    {
        _pathBuilder.AddPath(pathComponent, pathComponents);
        return this;
    }

    public IUriBuilder WithFragment(string fragment)
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
            return _uriBuilder.Uri;
        }
    }

    private readonly System.UriBuilder _uriBuilder;
    private readonly PathBuilder _pathBuilder = new();
    private readonly QueryBuilder _queryBuilder = new();
}