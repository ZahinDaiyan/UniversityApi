namespace UniversityApi.Models;

public class Student
{
  public int Id { get; set; }
  public required string Name { get; set; }

  public int Age { get; set; }

  // Foreign Key
  public int UniversityId { get; set; }

  // Navigation 
  public University University { get; set; } = null!;

}