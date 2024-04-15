namespace GameStoreAPI.dtos;

public record class GameDto(int Id, string Name,
                            string Type, decimal Price,
                            DateOnly ReleaseDate);