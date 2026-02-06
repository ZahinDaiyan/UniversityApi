namespace UniversityApi.DTOs;

public class CreateStudentDto
{
  public required string Name { get; set; }
  public required int Age { get; set; }
  public int UniversityId { get; set; }

}
