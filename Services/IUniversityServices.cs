using UniversityApi.DTOs;

namespace UniversityApi.Services;

public interface IUniversityService
{
	Task<UniversityResponseDto> CreateAsync(CreateUniversityDto dto);
	Task<IEnumerable<UniversityResponseDto>> GetAllAsync();
	Task<UniversityResponseDto?> GetByIdAsync(int id);
	Task<bool> UpdateAsync(int id, UpdateUniversityDto dto);
	Task<bool> DeleteAsync(int id);
}
