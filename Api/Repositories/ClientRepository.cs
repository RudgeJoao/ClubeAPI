using Api.Data;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly OracleDbContext _context;

    public ClientRepository(OracleDbContext context)
    {
        _context = context;
    }

    public async Task<List<Client>> GetClients()
    {
        return await _context.Client.ToListAsync();
    }

    public async Task<Client?> GetClientById(int id)
    {
        return await _context.Client.FindAsync(id);
    }

    public async Task CreateClient(Client client)
    {
        await _context.Client.AddAsync(client);
        await _context.SaveChangesAsync();
    }

    public async Task EditClient(int id, Client cliente)
    {
        if (!ClientExists(id))
        {
            return;
        }
        _context.Client.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteClient(int id)
    {
        var client = await _context.Client.FindAsync(id);
        if (client == null)
        {
            return;
        }
        _context.Client.Remove(client);
        await _context.SaveChangesAsync();
    }

    private bool ClientExists(int id)
    {
        return _context.Client.Any(client => client.Id == id);
    }
}