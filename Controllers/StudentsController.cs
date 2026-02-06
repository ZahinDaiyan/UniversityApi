using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.DTOs;
using UniversityApi.Models;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentsController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/students
    [HttpPost]
    public async Task<ActionResult<StudentResponseDto>> Create(CreateStudentDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var universityExits = await _context.Universities
        .AnyAsync(u => u.Id == dto.UniversityId, cancellationToken);

        if (!universityExits)
            return BadRequest(new { message = "University dose not exits" });

        var student = new Student
        {
            Name = dto.Name,
            Age = dto.Age,
            UniversityId = dto.UniversityId
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(
      nameof(GetById),
      new { id = student.Id },
      new StudentResponseDto
      {
          Id = student.Id,
          Name = student.Name,
          Age = student.Age,
          UniversityId = student.UniversityId
      }
  );

    }
    // GET: api/students
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentResponseDto>>> GetAll(CancellationToken cancellationToken)
    {
        var student = await _context.Students
        .Select(s => new StudentResponseDto
        {
            Id = s.Id,
            Name = s.Name,
            Age = s.Age,
            UniversityId = s.UniversityId
        })
        .ToListAsync(cancellationToken);

        return Ok(student);
    }

    //Get : api/students/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentResponseDto>> GetById(int id, CancellationToken cancellationToken)
    {

        var student = await _context.Students
        .Where(s => s.Id == id)
        .Select(s => new StudentResponseDto
        {
            Id = s.Id,
            Name = s.Name,
            Age = s.Age,
            UniversityId = s.UniversityId
        })
        .FirstOrDefaultAsync(cancellationToken);

        if (student == null)
            return NotFound(new { message = "Student dose not exit" });

        return Ok(student);
    }

    //Put : api/students/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateStudentDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var student = await _context.Students
        .FindAsync(new object[] { id }, cancellationToken);

        if (student == null)
            return NotFound(new { message = "Student Not Found" });

        var universityExits = await _context.Universities
        .AnyAsync(u => u.Id == dto.UniversityId, cancellationToken);

        if (!universityExits)
            return BadRequest(new { message = "University dose not exit" });

        student.Name = dto.Name;
        student.Age = dto.Age;
        student.UniversityId = dto.UniversityId;

        await _context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    // Delete: api/students/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var student = await _context.Students
        .FindAsync(new object[] { id }, cancellationToken);

        if (student == null)
            return NotFound(new { message = "Student dose not exit" });

        _context.Students.Remove(student);
        await _context.SaveChangesAsync(cancellationToken);


        return NoContent();
    }

}

