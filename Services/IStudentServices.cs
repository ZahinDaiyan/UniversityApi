using UniversityApi.DTOs;

namespace UniversityApi.Services;

public interface IStudentServices
{
  Task<StudentResponseDto> CreateAsync(CreateStudentDto dto, CancellationToken ct);
  Task<IEnumerable<StudentResponseDto>> GetAllAsync(CancellationToken ct);
  Task<StudentResponseDto?> GetByIdAsync(int it, CancellationToken ct);
  Task<bool> UpdateAsync(int id, UpdateStudentDto dto, CancellationToken ct);
  Task<bool> DeleteAsync(int it, CancellationToken ct);

}