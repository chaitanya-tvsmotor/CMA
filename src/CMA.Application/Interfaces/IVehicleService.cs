using CMA.Application.DTOs;

namespace CMA.Application.Interfaces;

public interface IVehicleService
{
    Task<IEnumerable<VehicleDto>> GetAllAsync();
    Task<VehicleDto?> GetByIdAsync(int id);
    Task<VehicleDto> CreateAsync(CreateVehicleDto dto);
    Task<VehicleDto?> UpdateAsync(int id, CreateVehicleDto dto);
    Task<VehicleDto?> AssignToEmployeeAsync(int vehicleId, string employeeId);
    Task<bool> DeleteAsync(int id);
}
