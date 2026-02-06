using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.DTOs;
using UniversityApi.Models;
namespace UniversityApi.Service;

public async UniversityService : IUniversityService
{
	private readonly AppDbContext _context;

	public UniversityService (AppDbContext context)
	{
		_context = context;
	}

	public async Task<UniversityResponseDto> CreateAsync(CreateUniversityDto dto, CancellationToken ct)
	{
		var university = new University
		{
			Name = dto.Name,
			CityName = dto.CityName
		};

		_context.Universities.Add(university);
		await _context.SaveChangesAysnce(ct);

		return new UniversityResponseDto
		{
			Id = university.Id,
			Name = university.Name,
			CityName = university.Cityname
		};
	}

	public async Task<IEnumerable<UniversityResponseDto>> GetAllAsync(CancellationToken ct)
	{
		return await _context.Universities
			.Select ( u => new UniversityResponseDto
					{
						Id = u.Id,
						Name = u.Name,
						CityName = u.CityName

					})
		.ToListAsync();


	}

	public async Task<UniversityResponseDto?> GetByIdAsync (int id, CancellationToken ct)
	{
		return await _context.Universities
			.Where( u => u.Id = id)
			.Select ( u => new UniversityResponseDto
					{
						Id = u.Id,
						Name = u.Name,
						CityName = u.CityName
					})
		.FirstOrDefaultAsync(ct);
	}

	public async Task<bool> UpdateAsync(int id,UpdateUniversityDto dto CancellationToken ct)
	{
		var university = await _context.Universities
			.FindAsync(id);
		if ( university == null)
			return false;
		university.Name = dto.Name;
		university.CityName = dto.CityName;

		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> DeleteAsync(int id , CancellationToken ct)
	{
		var university = await _context.Universities
			.Include( u => u.Students )
			.FirstOrDefaultAsync( u => u.Id == id);
		if (university == null) 
			return false;

		_context.Universities.Remove(university);
		await _context.SaveChangesAsync(ct);
		return true;
	}
}



