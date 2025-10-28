using HW4.DTOs;

namespace HW4.Services;

public interface IBookService
{
    Task<IEnumerable<BookResponseDto>> GetAllAsync();
    Task<BookResponseDto?> GetByIdAsync(int id);
    Task<BookResponseDto> CreateAsync(CreateBookDto dto);
    Task UpdateAsync(int id, UpdateBookDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<BookResponseDto>> GetPublishedAfterAsync(int year);
}