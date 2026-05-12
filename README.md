# Basic .NET C# API Endpoints

## Overview

A basic ASP.NET Core Web API project created based on this [YouTube Tutorial](https://www.youtube.com/watch?v=YbRe4iIVYJk).

## Tech Stack

- ASP.NET Core (.NET 10)
- C#
- SQLite

## Notes

- To overwrite connection string:

```
$env:ConnectionStrings__GameStore="Data Source=Production.db"
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
