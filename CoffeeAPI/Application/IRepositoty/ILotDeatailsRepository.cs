using Application.SeedWorks;
using Data.Entities;

namespace Application.IRepositoty
{
    public interface ILotDeatailsRepository : IRepository<LotDetails, int>
    {
        Task<List<LotDetails>> GetLotDetailsFindByDayAsync(DateTime fromDay, DateTime toDay);
    }
}
