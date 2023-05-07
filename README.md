[![Nuget](https://img.shields.io/nuget/v/Bromix.Fluent.Uri)](https://www.nuget.org/packages/Bromix.Fluent.Uri/) [![Nuget](https://img.shields.io/nuget/dt/Bromix.Fluent.Uri)](https://www.nuget.org/packages/Bromix.Fluent.Uri/) [![GitHub](https://img.shields.io/github/license/bromix/Bromix.Fluent.Uri)](https://github.com/bromix/Bromix.Fluent.Uri/blob/main/LICENSE)

# Bromix.Fluent.Uri

A .NET library that provides a fluent API for building URIs with strict validation of scheme, host, and port.

## Features
- Fluent API for building URIs
- Strict validation of scheme, host, and port
- Supports IPv4 address validation


## Installation
You can install the package via NuGet:

```
Install-Package Bromix.Fluent.Uri -Version 1.0.0
```

Or via the .NET CLI:
```
dotnet add package Bromix.Fluent.Uri --version 1.0.0
```

## Usage
### Creating a URI
```csharp
var uriBuilder = FluentUriBuilder
    .Create("https", "example.com")
    .WithPath("path")
    .WithQuery("key", "value");

var uri = uriBuilder.Uri;

Console.WriteLine(uri.ToString()); // "https://example.com/path?key=value"
```