namespace GameStoreAPI.dtos;

public record UpdateGameDto(
    string Name,
    string Type,
    decimal Price,
    DateOnly ReleaseDate
);