using Api.Models;

namespace Api.Interfaces;

public interface IClientRepository
{
    Task<List<Client>> GetClients();

    Task<Client?> GetClientById(int id);
    Task CreateClient(Client client);
    Task EditClient(int id, Client cliente);
    Task DeleteClient(int id);

}