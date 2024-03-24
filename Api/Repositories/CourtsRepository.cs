using Api.Data;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;
namespace Api.Repositories
{
    public class CourtsRepository : ICourtRepository
    {
        private readonly OracleDbContext _context;

        public CourtsRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<List<Court>> GetCourts()
        {
            return await _context.Courts.ToListAsync();
        }

        public async Task<Court?> GetCourtById(int id)
        {
            return await _context.Courts.FindAsync(id);
        }

        public async Task CreateCourt(Court court)
        {
            await _context.Courts.AddAsync(court);
            await _context.SaveChangesAsync();
        }

        public async Task EditCourt(int id, Court court)
        {
            var existingCourt = await _context.Courts.FindAsync(id);
            if (existingCourt == null)
            {
                return;
            }
            existingCourt.Id = court.Id;
            existingCourt.Name = court.Name;

            _context.Courts.Update(existingCourt);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourt(int id) 
        {
            var existingCourt = await _context.Courts.FindAsync(id);
            if (existingCourt == null)
            {
                return;
            }

            _context.Courts.Remove(existingCourt);
            await _context.SaveChangesAsync();
        }

    }
}
