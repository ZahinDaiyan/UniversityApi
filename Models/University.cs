namespace UniversityApi.Models;

public class University
{
  public int Id { get; set; }
  public string Name { get; set; } = null!;

  public string CityName { get; set; } = null!;

  // Navigation Property 
  public ICollection<Student> Students { get; set; } = new List<Student>();

}