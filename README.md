# Basic .NET C# API Endpoints

## Overview

A basic ASP.NET Core Web API project created based on this [YouTube Tutorial](https://www.youtube.com/watch?v=YbRe4iIVYJk). The project has been expanded to use SQL Server instead of SQLite.

## Tech Stack

- ASP.NET Core (.NET 10)
- C#
- SQL Server

## How to Run Locally

Please note that you must set up a SQL Server prior to running this project. Although this project was intended to use a Windows Server VM running an SQL Server, you can use a SQL Server locally, in Docker, or hosted in the cloud.

- Once the project is on your local machine, set the environment variable by running the following command in root with information pointing to the SQL Server:

```
$env:ConnectionStrings__GameStore="Server=<IPv4>,<Port>;Database=GameStore;User Id=<Server Name>;Password=<Password>;TrustServerCertificate=True;"
```

- After, run the next command:

```
dotnet run
```

- In your browser, go to the localhost url provided from the terminal (i.e. http://local:5209)

## Deploying to a Virtual Machine

- Using the terminal, run the following command in root:

```
dotnet publish -c Release
```

- Transfer the publish folder contents to C:\inetpub\wwwroot\\\<AppName\> in Virtual Machine
- Set connection string on Windows System or IIS environment variables
  - Although you can update the JSON settings file, it is better practice to update the connection string outside of the publish folder
- Additional set up maybe required to properly update permissions to allow program to run

## Notes

- Example body for the create game POST endpoint:

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
