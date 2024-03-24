using Api.Models;

namespace Api.Interfaces
{
    public interface ICourtRepository
    {
        Task<List<Court>> GetCourts();
        Task<Court?> GetCourtById(int id);
        Task CreateCourt(Court court);
        Task EditCourt(int id, Court court);
        Task DeleteCourt(int id);
    }
}
