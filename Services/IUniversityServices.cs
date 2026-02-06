using UniversityApi.Dto;

namespace UniversityApi.Service;

public interface IUniversityService
{
	Task<UniversityResponseDto> CreateAsync(CreateUniversityDto dto, CancellationToken ct);
	Task<IEnumerable<UniversityResponseDto>> GetAllAsync (CancellationToken ct);
	Task<bool> UpdateAsync(int id, UpdateUniversityDto dto, CancellationToken ct);
	Task<bool> DeleteAsync(int id, CancellationToken ct);

}
