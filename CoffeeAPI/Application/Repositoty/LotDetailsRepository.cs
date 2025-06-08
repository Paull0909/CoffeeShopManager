using Application.IRepositoty;
using Application.Service;
using AutoMapper;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositoty
{
    public class LotDetailsRepository : RepositoryBase<LotDetails, int>, ILotDeatailsRepository
    {
        private readonly IMapper _mapper;

        public LotDetailsRepository(Web_Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<LotDetails>> GetLotDetailsFindByDayAsync(DateTime fromDay, DateTime toDay)
        {
            var list = await _context.LotsDetails.ToListAsync();
            var result = list.Where(t => t.CreateAt >= fromDay && t.CreateAt <= toDay).ToList();
            return result;
        }
    }
}
