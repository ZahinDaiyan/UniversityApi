using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.DTOs;
using UniversityApi.Models;

namespace UniversityApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UniversitiesController : ControllerBase
{
    private readonly AppDbContext _context;

    public UniversitiesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/universities
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UniversityResponseDto>>> GetAll()
    {
        var universities = await _context.Universities
            .Select(u => new UniversityResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                CityName = u.CityName
            })
            .ToListAsync();

        return Ok(universities);
    }

    // GET: api/universities/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UniversityResponseDto>> GetById(int id)
    {
        var university = await _context.Universities.FindAsync(id);

        if (university == null) return NotFound(new { message = "University not found" });

        return Ok(new UniversityResponseDto
        {
            Id = university.Id,
            Name = university.Name,
            CityName = university.CityName
        });
    }

    // POST: api/universities
    [HttpPost]
    public async Task<ActionResult<UniversityResponseDto>> Create([FromBody] CreateUniversityDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var university = new University
        {
            Name = dto.Name,
            CityName = dto.CityName
        };

        _context.Universities.Add(university);
        await _context.SaveChangesAsync();

        var response = new UniversityResponseDto
        {
            Id = university.Id,
            Name = university.Name,
            CityName = university.CityName
        };

        return CreatedAtAction(nameof(GetById), new { id = university.Id }, response);
    }

    // PUT: api/universities/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUniversityDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var university = await _context.Universities.FindAsync(id);
        if (university == null) return NotFound(new { message = "University not found" });

        university.Name = dto.Name;
        university.CityName = dto.CityName;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/universities/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var university = await _context.Universities.Include(u => u.Students).FirstOrDefaultAsync(u => u.Id == id);
        if (university == null) return NotFound(new { message = "University not found" });

        _context.Universities.Remove(university);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
