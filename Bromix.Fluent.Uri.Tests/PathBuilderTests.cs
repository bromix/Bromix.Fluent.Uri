namespace Bromix.Fluent.Uri.Tests;

public sealed class PathBuilderTests
{
    [Theory]
    [InlineData("home")]
    [InlineData("/home")]
    [InlineData("home/")]
    [InlineData("/home/")]
    public void AddPath_With_PathComponents_Should_BeValid(string path)
    {
        new PathBuilder()
            .AddPath(path)
            .Path
            .Should().Be("home");
    }

    [Theory]
    [InlineData("home/1")]
    [InlineData("/home/1")]
    [InlineData("home/1/")]
    [InlineData("/home/1/")]
    public void AddPath_With_TwoPathComponents_Should_BeValid(string path)
    {
        new PathBuilder()
            .AddPath(path)
            .Path
            .Should().Be("home/1");
    }

    [Theory]
    [InlineData("home")]
    [InlineData("/home")]
    [InlineData("home/")]
    [InlineData("/home/")]
    public void SetPath_With_PathComponents_Should_BeValid(string path)
    {
        new PathBuilder()
            .SetPath(path)
            .Path
            .Should().Be("home");
    }
}