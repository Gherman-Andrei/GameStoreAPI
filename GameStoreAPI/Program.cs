using GameStoreAPI.dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetgameEndpointName = "GetGame";
List<GameDto> games =
[
    new(1, "Joc1", "Tip1", 30.29M, new DateOnly(2012, 2, 12)),
    new(2, "Joc2", "Tip2", 33.29M, new DateOnly(2022, 2, 12)),
    new(3, "Joc3", "Tip3", 305.29M, new DateOnly(2013, 2, 12))
];
// GET /games
app.MapGet("games", () => games);
// GET /games/id
app.MapGet("games/{id}", (int id) =>
{
    GameDto ? game = games.Find(game => game.Id == id);
    return game is null ? Results.NotFound() : Results.Ok(game);
});
// POST /games
app.MapPost("games", (CreateGameDto newGame) =>
{
    GameDto game = new(games.Count +1, newGame.Name,newGame.Type, newGame.Price,newGame.ReleaseDate);
    games.Add(game);

    return Results.CreatedAtRoute(GetgameEndpointName, new{id = game.Id}, game);
});
//PUT /games/id
app.MapPut("games/{id}", (int id, UpdateGameDto updategame) =>
{
    var index = games.FindIndex(game => game.Id == id);

    if (index == -1)
    {
        return Results.NotFound();
    }
    games[index] = new GameDto(
        id,
        updategame.Name,
        updategame.Type,
        updategame.Price,
        updategame.ReleaseDate
        );
    return Results.NoContent();
});

// DELETE

app.MapDelete("games/{id}", (int id) =>
{
    games.RemoveAll(game => game.Id == id);
    return Results.NoContent();
});
app.Run();