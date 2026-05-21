# Basic .NET C# API Endpoints

## Overview

A basic ASP.NET Core Web API project created based on this [YouTube Tutorial](https://www.youtube.com/watch?v=YbRe4iIVYJk). The project has been expanded to use SQL Server instead of SQLite.

## Tech Stack

- ASP.NET Core (.NET 10)
- C#
- SQL Server

## Notes

- For production, set connection string on Windows System or IIS environment variables
- For testing and development, set connection string environment variable with the terminal in root:

```
$env:ConnectionStrings__GameStore="Server=<IPv4>,<Port>;Database=GameStore;User Id=<Server Name>;Password=<Password>;TrustServerCertificate=True;"
```

- Example body for POST requests:

```
{
    "Name":"Final Fantasy VII Rebirth",
    "GenreId": 2,
    "Price": 69.99,
    "ReleaseDate": "2024-02-21"
}
```

- When switching database type, run migrations with following command in root:

```
dotnet ef migrations add InitialCreate --output-dir Data/Migrations
```
