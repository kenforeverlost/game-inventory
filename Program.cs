using GameInventory.Data;
using GameInventory.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();
builder.AddGameStoreDb();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGamesEndpoints();
app.MapGenreEndpoints();

app.MigrateDb();

app.Run();
