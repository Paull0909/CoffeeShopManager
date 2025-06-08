using Application.IRepositoty;
using Application.Service;
using AutoMapper;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositoty
{
    public class LotRepository : RepositoryBase<Lot, int>, ILotRepository
    {
        private readonly IMapper _mapper;

        public LotRepository(Web_Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<LotDetails> GetLotDetailsByLotId(int lotId)
        {
            var result = await _context.LotsDetails.
                Where(t => t.LotId == lotId)
                .OrderByDescending(t => t.Id)
                .FirstOrDefaultAsync();
            return result;
        }
    }
}
