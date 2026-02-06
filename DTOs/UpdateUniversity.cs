using System.ComponentModel.DataAnnotations;
namespace UniversityApi.DTOs;

public class UpdateUniversityDto
{

  [Required]
  [MaxLength(100)]
  public string Name { get; set; } = null!;

  [Range(1, 120)]
  public string CityName { get; set; } = null!;

}
