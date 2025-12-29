using CMA.Application.DTOs;

namespace CMA.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<IEnumerable<UserDto>> GetByRoleAsync(string role);
    Task<UserDto?> GetByIdAsync(string id);
    Task<bool> UpdateAsync(string id, UserDto dto);
    Task<bool> DeleteAsync(string id);
}
