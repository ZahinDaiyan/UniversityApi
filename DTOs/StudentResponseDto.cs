using System;

namespace UniversityApi.DTOs;

public class StudentResponseDto
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public int Age { get; set; }
  public int UniversityId { get; set; }
}
