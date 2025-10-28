namespace HW4.DTOs;

public record CreateBookDto(
    string Title,
    int PublishedYear,
    int AuthorId
);

public record UpdateBookDto(
    string Title,
    int PublishedYear,
    int AuthorId
);

public record BookResponseDto(
    int Id,
    string Title,
    int PublishedYear,
    int AuthorId
);