using HW4.DTOs;

namespace HW4.Services;

public interface IBookService
{
    IEnumerable<BookResponseDto> GetAll();
    BookResponseDto? GetById(int id);
    BookResponseDto Create(CreateBookDto dto);
    void Update(int id, UpdateBookDto dto);
    void Delete(int id);
}