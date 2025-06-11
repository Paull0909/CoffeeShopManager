using Application.SeedWorks;
using Data.Entities;

namespace Application.IRepositoty
{
    public interface IPositionsRepository : IRepository<Positions, int>
    {
        Task<Positions> GetPositionByUserIdAsync(Guid userid);
    }
}
