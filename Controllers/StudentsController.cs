using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.DTOs;
using UniversityApi.Models;
using UniversityApi.Services;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
	private readonly IStudentServices _service;

	public StudentsController(IStudentServices service)
	{
		_service = service;
	}

	[HttpPost]
	public async Task<ActionResult<StudentResponseDto>> Create(
			CreateStudentDto dto,
			CancellationToken ct)
	{
		try
		{
			var student = await _service.CreateAsync(dto, ct);
			return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
		}
		catch (InvalidOperationException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<StudentResponseDto>>> GetAll(CancellationToken ct)
			=> Ok(await _service.GetAllAsync(ct));

	[HttpGet("{id:int}")]
	public async Task<ActionResult<StudentResponseDto>> GetById(int id, CancellationToken ct)
	{
		var student = await _service.GetByIdAsync(id, ct);
		return student == null
				? NotFound(new { message = "Student not found" })
				: Ok(student);
	}

	[HttpPut("{id:int}")]
	public async Task<IActionResult> Update(int id, UpdateStudentDto dto, CancellationToken ct)
	{
		try
		{
			var updated = await _service.UpdateAsync(id, dto, ct);
			return updated ? NoContent() : NotFound();
		}
		catch (InvalidOperationException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id, CancellationToken ct)
			=> await _service.DeleteAsync(id, ct) ? NoContent() : NotFound();
}

