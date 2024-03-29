using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class RentRepository
{
    private readonly OracleDbContext _context;

    public RentRepository(OracleDbContext context)
    {
        _context = context;
    }

    public async Task<List<Rent?>> GetRents()
    {
        return await _context.Rents.ToListAsync();
    }

    public async Task<Rent?> GetRentById(int id)
    {
        return await _context.Rents.FindAsync(id);
    }

    public async Task CreateRent(Rent rent)
    {
        await _context.Rents.AddAsync(rent);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRent(int id)
    {
        var rent = await _context.Rents.FindAsync(id);
        if (rent == null)
        {
            return;
        }
        _context.Rents.Remove(rent);
        _context.SaveChanges();
    }
}