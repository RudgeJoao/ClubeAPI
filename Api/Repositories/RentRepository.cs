using Api.Data;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class RentRepository : IRentRepository
{
    private readonly OracleDbContext _context;

    public RentRepository(OracleDbContext context)
    {
        _context = context;
    }

    public async Task<List<Rent?>> GetRents()
    {
        var rents = await _context.Rents
            .Include(rent => rent.Client)
            .Include(rent => rent.Court)
            .ToListAsync();
        return rents;
    }

    public async Task<Rent?> GetRentById(int id)
    {
        var rent = await _context.Rents
            .Include(rent => rent.Client)
            .Include(rent => rent.Court)
            .FirstAsync(rent => rent.Id == id);
        return rent;
    }

    public async Task CreateRent(Rent rent)
    {
        await _context.Rents.AddAsync(rent);
        await _context.SaveChangesAsync();
    }

    public async Task EditRent(int id, Rent rent)
    {
        var existingRent = await _context.Rents.FindAsync(id);
        if (existingRent == null)
            return;

        existingRent.Id = rent.Id;
        existingRent.Court = rent.Court;
        existingRent.Client = rent.Client;
        existingRent.Start = rent.Start;
        existingRent.End = rent.End;
    }

    public async Task DeleteRent(int id)
    {
        var rent = await _context.Rents.FindAsync(id);
        if (rent == null)
        {
            return;
        }
        _context.Rents.Remove(rent);
        await _context.SaveChangesAsync();
    }
}