using UniversityApi.Models;

namespace UniversityApi.DTOs;

public class RegisterDto
{
  public string Username { get; set; } = null!;
  public string Password { get; set; } = null!;
}