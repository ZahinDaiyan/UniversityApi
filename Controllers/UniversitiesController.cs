using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.DTOs;
using UniversityApi.Models;
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
    public async Task<ActionResult<IEnumerable<UniversityResponseDto>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UniversityResponseDto>> GetById(int id)
    {
        var uni = await _service.GetByIdAsync(id);
        return uni == null ? NotFound(new { message = "University not found" }) : Ok(uni);
    }

    [HttpPost]
    public async Task<ActionResult<UniversityResponseDto>> Create(CreateUniversityDto dto)
    {
        var uni = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = uni.Id }, uni);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateUniversityDto dto)
        => await _service.UpdateAsync(id, dto) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => await _service.DeleteAsync(id) ? NoContent() : NotFound();
}
