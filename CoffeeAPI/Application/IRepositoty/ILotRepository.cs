using Application.SeedWorks;
using Data.Entities;

namespace Application.IRepositoty
{
    public interface ILotRepository : IRepository<Lot, int>
    {
        public Task<LotDetails> GetLotDetailsByLotId(int lotId);
    }
}
