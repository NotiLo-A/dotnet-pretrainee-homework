using HW4.DTOs;

namespace HW4.Services;

public interface IAuthorService
{
    IEnumerable<AuthorResponseDto> GetAll();
    AuthorResponseDto? GetById(int id);
    AuthorResponseDto Create(CreateAuthorDto dto);
    void Update(int id, UpdateAuthorDto dto);
    void Delete(int id);
}