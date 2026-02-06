using UniversityApi.DTOs;

namespace UniversityApi.Services;

public interface IUniversityService
{
	Task<UniversityResponseDto> CreateAsync(CreateUniversityDto dto, CancellationToken ct);
	Task<IEnumerable<UniversityResponseDto>> GetAllAsync(CancellationToken ct);
	Task<UniversityResponseDto?> GetByIdAsync(int id, CancellationToken ct);
	Task<bool> UpdateAsync(int id, UpdateUniversityDto dto, CancellationToken ct);
	Task<bool> DeleteAsync(int id, CancellationToken ct);
}
