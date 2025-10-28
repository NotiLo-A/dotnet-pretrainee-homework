using HW4.DTOs;

namespace HW4.Services;

public interface IAuthorService
{
    Task<IEnumerable<AuthorResponseDto>> GetAllAsync();
    Task<AuthorResponseDto?> GetByIdAsync(int id);
    Task<AuthorResponseDto> CreateAsync(CreateAuthorDto dto);
    Task UpdateAsync(int id, UpdateAuthorDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<AuthorWithBookCountDto>> GetAllWithBookCountsAsync();
    Task<IEnumerable<AuthorResponseDto>> SearchByNameAsync(string query, bool startsWith);
}