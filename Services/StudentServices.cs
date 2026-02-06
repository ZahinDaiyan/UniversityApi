using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.DTOs;
using UniversityApi.Models;

namespace UniversityApi.Services;

public class StudentService : IStudentServices
{
  private readonly AppDbContext _context;

  public StudentService(AppDbContext context)
  {
    _context = context;
  }

  public async Task<StudentResponseDto> CreateAsync(CreateStudentDto dto, CancellationToken ct)
  {
    var universityExists = await _context.Universities
        .AnyAsync(u => u.Id == dto.UniversityId, ct);

    if (!universityExists)
      throw new InvalidOperationException("University does not exist");

    var student = new Student
    {
      Name = dto.Name,
      Age = dto.Age,
      UniversityId = dto.UniversityId
    };

    _context.Students.Add(student);
    await _context.SaveChangesAsync(ct);

    return new StudentResponseDto
    {
      Id = student.Id,
      Name = student.Name,
      Age = student.Age,
      UniversityId = student.UniversityId
    };
  }

  public async Task<IEnumerable<StudentResponseDto>> GetAllAsync(CancellationToken ct)
  {
    return await _context.Students
        .Select(s => new StudentResponseDto
        {
          Id = s.Id,
          Name = s.Name,
          Age = s.Age,
          UniversityId = s.UniversityId
        })
        .ToListAsync(ct);
  }

  public async Task<StudentResponseDto?> GetByIdAsync(int id, CancellationToken ct)
  {
    return await _context.Students
        .Where(s => s.Id == id)
        .Select(s => new StudentResponseDto
        {
          Id = s.Id,
          Name = s.Name,
          Age = s.Age,
          UniversityId = s.UniversityId
        })
        .FirstOrDefaultAsync(ct);
  }

  public async Task<bool> UpdateAsync(int id, UpdateStudentDto dto, CancellationToken ct)
  {
    var student = await _context.Students.FindAsync(new object[] { id }, ct);
    if (student == null)
      return false;

    var universityExists = await _context.Universities
        .AnyAsync(u => u.Id == dto.UniversityId, ct);

    if (!universityExists)
      throw new InvalidOperationException("University does not exist");

    student.Name = dto.Name;
    student.Age = dto.Age;
    student.UniversityId = dto.UniversityId;

    await _context.SaveChangesAsync(ct);
    return true;
  }

  public async Task<bool> DeleteAsync(int id, CancellationToken ct)
  {
    var student = await _context.Students.FindAsync(new object[] { id }, ct);
    if (student == null)
      return false;

    _context.Students.Remove(student);
    await _context.SaveChangesAsync(ct);
    return true;
  }
}
