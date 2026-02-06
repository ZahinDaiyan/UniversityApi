using Microsoft.AspNetCore.Mvc;
using UniversityApi.DTOs;
using UniversityApi.Services;

namespace UniversityApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UniversitiesController : ControllerBase
{
    private readonly IUniversityService _service;

    public UniversitiesController(IUniversityService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UniversityResponseDto>>> GetAll(
        CancellationToken ct)
        => Ok(await _service.GetAllAsync(ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UniversityResponseDto>> GetById(
        int id,
        CancellationToken ct)
    {
        var uni = await _service.GetByIdAsync(id, ct);
        return uni == null
            ? NotFound(new { message = "University not found" })
            : Ok(uni);
    }

    [HttpPost]
    public async Task<ActionResult<UniversityResponseDto>> Create(
        CreateUniversityDto dto,
        CancellationToken ct)
    {
        var uni = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = uni.Id }, uni);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateUniversityDto dto,
        CancellationToken ct)
        => await _service.UpdateAsync(id, dto, ct) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken ct)
        => await _service.DeleteAsync(id, ct) ? NoContent() : NotFound();
}
