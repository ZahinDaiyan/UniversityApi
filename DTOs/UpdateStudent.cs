using System.ComponentModel.DataAnnotations;
namespace UniversityApi.DTOs;

public class UpdateStudentDto
{

  [Required]
  [MaxLength(100)]
  public string Name { get; set; } = null!;

  [Range(1, 120)]
  public int Age { get; set; }

  [Required]
  public int UniversityId { get; set; }
}
