using Application.SeedWorks;
using Data.Entities;

namespace Application.IRepositoty
{
    public interface IToppingsRepository : IRepository<Toppings, int>
    {
        Task<List<Toppings>> GetAllIsAvailable();
    }
}
