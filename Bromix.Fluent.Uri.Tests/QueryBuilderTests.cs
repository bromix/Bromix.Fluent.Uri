using System.Collections.Specialized;

namespace Bromix.Fluent.Uri.Tests;

public sealed class QueryBuilderTests
{
    [Fact]
    public void QueryBuilder_Is_Empty()
    {
        new QueryBuilder()
            .Query
            .Should().Be(string.Empty);
    }
    
    [Fact]
    public void QueryBuilder_Add_One_Value()
    {
        new QueryBuilder()
            .Add("hello", "world")
            .Query
            .Should().Be("hello=world");
    }
    
    [Fact]
    public void QueryBuilder_Add_Two_Values()
    {
        new QueryBuilder()
            .Add("hello", "world")
            .Add("name", "john")
            .Query
            .Should().Be("hello=world&name=john");
    }
    
    [Fact]
    public void QueryBuilder_Add_Two_Values_Via_NameValueCollection()
    {
        var values = new NameValueCollection{{"hello", "world"}, {"name", "john"}};
        new QueryBuilder()
            .Add(values)
            .Query
            .Should().Be("hello=world&name=john");
    }
    
    [Fact]
    public void QueryBuilder_Add_Values_Of_The_Same_Name()
    {
        new QueryBuilder()
            .Add("id", "1")
            .Add("id", "2")
            .Query
            .Should().Be("id=1&id=2");
    }
}