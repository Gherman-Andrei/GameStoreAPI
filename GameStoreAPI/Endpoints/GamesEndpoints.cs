using GameStoreAPI.dtos;

namespace GameStoreAPI.Endpoints;

public static class GamesEndpoints
{
    const string GetgameEndpointName = "GetGame";
    
    private static readonly List<GameDto> games =
    [
        new(1, "Joc1", "Tip1", 30.29M, new DateOnly(2012, 2, 12)),
        new(2, "Joc2", "Tip2", 33.29M, new DateOnly(2022, 2, 12)),
        new(3, "Joc3", "Tip3", 305.29M, new DateOnly(2013, 2, 12))
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("games");
        // GET /games
        group.MapGet("/", () => games);
// GET /games/id
        group.MapGet("/{id}", (int id) =>
        {
            GameDto ? game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        });
// POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(games.Count +1, newGame.Name,newGame.Type, newGame.Price,newGame.ReleaseDate);
            games.Add(game);

            return Results.CreatedAtRoute(GetgameEndpointName, new{id = game.Id}, game);
        });
//PUT /games/id
        group.MapPut("/{id}", (int id, UpdateGameDto updategame) =>
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

        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });

        return group;
    }

}