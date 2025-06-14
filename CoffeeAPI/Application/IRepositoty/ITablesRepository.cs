using Application.SeedWorks;
using Data.Entities;
using Data.Enum;

namespace Application.IRepositoty
{
    public interface ITablesRepository : IRepository<Tables, int>
    {
        Task<Tables> updateStatus(int id, TableStatus status);
        Task<List<Tables>> GetOrderNumberTagByStatus(TableStatus status);
    }
}
