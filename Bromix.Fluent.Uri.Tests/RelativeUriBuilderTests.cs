namespace Bromix.Fluent.Uri.Tests;

public sealed class RelativeUriBuilderTests
{
    [Fact]
    public void Empty()
    {
        FluentUriBuilder
            .CreateRelative()
            .Uri
            .ToString()
            .Should().Be("/");
    }

    [Fact]
    public void Add_Path_Component()
    {
        FluentUriBuilder
            .CreateRelative()
            .AddPath("Item")
            .Uri
            .ToString()
            .Should().Be("/Item");
    }

    [Fact]
    public void Add_Path_Components()
    {
        FluentUriBuilder
            .CreateRelative()
            .AddPath("Item", "12345")
            .Uri
            .ToString()
            .Should().Be("/Item/12345");
    }

    [Fact]
    public void Add_Path_Component_And_Query_Parameter()
    {
        FluentUriBuilder
            .CreateRelative()
            .AddPath("Item")
            .AddQuery("id", "12345")
            .Uri
            .ToString()
            .Should().Be("/Item?id=12345");
    }

    [Fact]
    public void Add_Path_Component_And_Query_Parameters_With_Same_Name()
    {
        FluentUriBuilder
            .CreateRelative()
            .AddPath("Item")
            .WithQuery(builder => builder
                .Add("id", "12345")
                .Add("id", "54321"))
            .Uri
            .ToString()
            .Should().Be("/Item?id=12345&id=54321");
    }
}