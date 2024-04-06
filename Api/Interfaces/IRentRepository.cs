using Api.Models;

namespace Api.Interfaces;

public interface IRentRepository
{
    Task<List<Rent?>> GetRents();
    Task<Rent?> GetRentById(int id);
    Task CreateRent(Rent rent);
    Task EditRent(int id, Rent rent);
    Task DeleteRent(int id);
}