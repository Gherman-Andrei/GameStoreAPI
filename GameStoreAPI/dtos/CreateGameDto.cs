namespace GameStoreAPI.dtos;

public record class CreateGameDto(
        string Name,
        string Type,
        decimal Price,
        DateOnly ReleaseDate
    );

