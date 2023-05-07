namespace Bromix.Fluent.Uri.Tests;

public sealed class UriBuilderTests
{
    #region Scheme

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_Should_ThrowException_When_SchemeIsNullOrEmpty(string scheme)
    {
        // Given
        // No additional context is needed for this test.

        // When
        var action = () => FluentUriBuilder.Create(scheme, "localhost");

        // Then
        action.Should().Throw<ArgumentException>().WithMessage("Scheme cannot be null or empty*");
    }

    [Theory]
    [InlineData("http")]
    [InlineData("https")]
    [InlineData("ftp")]
    [InlineData("foo")]
    [InlineData("BAR")]
    public void Create_Should_NotThrow_When_SchemeIsValid(string scheme)
    {
        // Given
        // No additional context is needed for this test.

        // When
        var action = () => FluentUriBuilder.Create(scheme, "localhost");

        // Then
        action.Should().NotThrow();
    }

    [Theory]
    [InlineData("1http")]
    [InlineData("+http")]
    [InlineData("-http")]
    [InlineData(".http")]
    public void Create_Should_ThrowException_When_SchemeStartsWithNonLetter(string scheme)
    {
        // Given
        // No additional context is needed for this test.

        // When
        var action = () => FluentUriBuilder.Create(scheme, "localhost");

        // Then
        action.Should().Throw<ArgumentException>().WithMessage("Scheme must start with a letter*");
    }

    [Theory]
    [InlineData("http+")]
    [InlineData("http-")]
    [InlineData("http.")]
    public void Create_Should_ThrowException_When_SchemeEndsWithNonLetter(string scheme)
    {
        // Given
        // No additional context is needed for this test.

        // When
        var action = () => FluentUriBuilder.Create(scheme, "localhost");

        // Then
        action.Should().Throw<ArgumentException>().WithMessage("Scheme must end with a letter or digit*");
    }

    [Theory]
    [InlineData("ht,tp")]
    [InlineData("ht;tp")]
    public void Create_Should_ThrowException_When_SchemeContainsInvalidCharacters(string scheme)
    {
        // Given
        // No additional context is needed for this test.

        // When
        var action = () => FluentUriBuilder.Create(scheme, "localhost");

        // Then
        action.Should().Throw<ArgumentException>().WithMessage("Scheme contains invalid character*");
    }

    #endregion

    #region Host

    [Fact]
    public void Create_Should_ThrowException_When_HostIsNull()
    {
        // Given
        string host = null!;

        // When
        var action = () => FluentUriBuilder.Create("http", host);

        // Then
        action.Should().Throw<ArgumentException>().WithMessage("Host must not be null or empty*");
    }

    [Fact]
    public void Create_Should_ThrowException_When_HostIsEmpty()
    {
        // Given
        var host = "";

        // When
        var action = () => FluentUriBuilder.Create("http", host);

        // Then
        action.Should().Throw<ArgumentException>().WithMessage("Host must not be null or empty*");
    }

    [Fact]
    public void Create_Should_ThrowException_When_HostIsInvalidDNSName()
    {
        // Given
        const string host = "example.-com";

        // When
        var action = () => FluentUriBuilder.Create("http", host);

        // Then
        action.Should().Throw<ArgumentException>().WithMessage("Invalid character '-' in DNS name format '-com'*");
    }

    [Theory]
    [InlineData("example.com")]
    [InlineData("www.example.com")]
    public void Create_Should_NotThrow_When_HostIsValidDNSName(string host)
    {
        // Given

        // When
        var action = () => FluentUriBuilder.Create("http", host);

        // Then
        action.Should().NotThrow();
    }

    [Theory]
    [InlineData("256.0.0.1")]
    [InlineData("1.256.0.1")]
    [InlineData("1.1.256.1")]
    [InlineData("1.1.1.256")]
    public void Create_Should_ThrowException_When_HostIsInvalidIPv4Address(string host)
    {
        // Given

        // When
        var action = () => FluentUriBuilder.Create("http", host);

        // Then
        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Invalid IPv4 address format. The value must *");
    }

    [Fact]
    public void Create_Should_NotThrow_When_HostIsValidIPv4Address()
    {
        // Given
        const string host = "192.168.0.1";

        // When
        var action = () => FluentUriBuilder.Create("http", host);

        // Then
        action.Should().NotThrow();
    }

    #endregion

    #region Port

    [Theory]
    [InlineData(0)]
    [InlineData(8080)]
    public void Create_Should_NotThrow_When_PostIsValid(int port)
    {
        // Arrange

        // Act
        Action action = () => FluentUriBuilder.Create("http", "localhost").WithPort(port);

        // Assert
        action.Should().NotThrow();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(70000)]
    public void Create_Should_ThrowException_When_PortIsInvalid(int port)
    {
        // Act
        Action action = () => FluentUriBuilder.Create("http", "localhost").WithPort(port);

        // Assert
        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Port number must be between 0 and 65535*")
            .And.ActualValue.Should().Be(port);
    }

    #endregion

    [Fact]
    public void Create_With_UserName()
    {
        FluentUriBuilder.Create("http", "localhost")
            .WithUserName("john")
            .Uri
            .ToString()
            .Should().Be("http://john@localhost/");
    }

    [Fact]
    public void Create_With_UserNameAndPassword()
    {
        FluentUriBuilder.Create("http", "localhost")
            .WithUserName("john", "1234")
            .Uri
            .ToString()
            .Should().Be("http://john:1234@localhost/");
    }

    [Fact]
    public void Create_With_Port()
    {
        FluentUriBuilder.Create("http", "localhost")
            .WithPort(1234)
            .Uri
            .ToString()
            .Should().Be("http://localhost:1234/");
    }

    [Fact]
    public void Create_With_PathComponent()
    {
        FluentUriBuilder.Create("http", "localhost")
            .AddPath("Item")
            .Uri
            .ToString()
            .Should().Be("http://localhost/Item");
    }

    [Fact]
    public void Create_With_PathComponents()
    {
        FluentUriBuilder.Create("http", "localhost")
            .AddPath("Item", "12345")
            .Uri
            .ToString()
            .Should().Be("http://localhost/Item/12345");
    }

    [Fact]
    public void Create_With_PathComponent_And_QueryParameter()
    {
        FluentUriBuilder.Create("http", "localhost")
            .AddPath("Item")
            .AddQuery("id", "12345")
            .Uri
            .ToString()
            .Should().Be("http://localhost/Item?id=12345");
    }

    [Fact]
    public void Create_With_PathComponent_And_QueryParameters_WithSameName()
    {
        FluentUriBuilder.Create("http", "localhost")
            .AddPath("Item")
            .WithQuery(builder => builder
                .Add("id", "12345")
                .Add("id", "54321"))
            .Uri
            .ToString()
            .Should().Be("http://localhost/Item?id=12345&id=54321");
    }
}