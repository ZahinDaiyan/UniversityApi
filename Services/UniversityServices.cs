using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.DTOs;
using UniversityApi.Models;

namespace UniversityApi.Services;

public class UniversityService : IUniversityService
{
	private readonly AppDbContext _context;

	public UniversityService(AppDbContext context)
	{
		_context = context;
	}

	public async Task<UniversityResponseDto> CreateAsync(CreateUniversityDto dto)
	{
		var university = new University
		{
			Name = dto.Name,
			CityName = dto.CityName
		};

		_context.Universities.Add(university);
		await _context.SaveChangesAsync();

		return new UniversityResponseDto
		{
			Id = university.Id,
			Name = university.Name,
			CityName = university.CityName
		};
	}

	public async Task<IEnumerable<UniversityResponseDto>> GetAllAsync()
	{
		return await _context.Universities
				.Select(u => new UniversityResponseDto
				{
					Id = u.Id,
					Name = u.Name,
					CityName = u.CityName
				})
				.ToListAsync();
	}

	public async Task<UniversityResponseDto?> GetByIdAsync(int id)
	{
		return await _context.Universities
				.Where(u => u.Id == id)
				.Select(u => new UniversityResponseDto
				{
					Id = u.Id,
					Name = u.Name,
					CityName = u.CityName
				})
				.FirstOrDefaultAsync();
	}

	public async Task<bool> UpdateAsync(int id, UpdateUniversityDto dto)
	{
		var university = await _context.Universities.FindAsync(id);
		if (university == null) return false;

		university.Name = dto.Name;
		university.CityName = dto.CityName;

		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var university = await _context.Universities
				.Include(u => u.Students) // ensure cascade removal or check business rules
				.FirstOrDefaultAsync(u => u.Id == id);

		if (university == null) return false;

		_context.Universities.Remove(university);
		await _context.SaveChangesAsync();
		return true;
	}
}
