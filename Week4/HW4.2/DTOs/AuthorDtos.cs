namespace HW4.DTOs;

public record CreateAuthorDto(
    string Name,
    DateOnly DateOfBirth
);

public record UpdateAuthorDto(
    string Name,
    DateOnly DateOfBirth
);

public record AuthorResponseDto(
    int Id,
    string Name,
    DateOnly DateOfBirth
);

public record AuthorWithBookCountDto(
    int Id,
    string Name,
    int BookCount
);