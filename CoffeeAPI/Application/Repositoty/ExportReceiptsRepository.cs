using Application.IRepositoty;
using Application.Service;
using AutoMapper;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositoty
{
    class ExportReceiptsRepository : RepositoryBase<ExportReceipts, int>,IExportReceiptsRepository
    {
        private readonly IMapper _mapper;

        public ExportReceiptsRepository(Web_Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<ExportDetails>> GetExportDetails(int exportId)
        {
            var resutl = await _context.ExportDetails.Where(t => t.ExportID == exportId).ToListAsync();
            return resutl;
        }
    }
}
