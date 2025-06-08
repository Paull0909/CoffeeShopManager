using Application.IRepositoty;
using Application.Service;
using AutoMapper;
using Data.Context;
using Data.DTO.ImportDetails;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositoty
{
    class ImportReceiptsRepository : RepositoryBase<ImportReceipts ,int>,IImportReceiptsRepository
    {
        private readonly IMapper _mapper;
        public ImportReceiptsRepository(Web_Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ImportReceipts> GetImportByFindIdAsync(int importId)
        {
            var list = await _context.ImportReceipts.ToListAsync();
            var result = list.Where(t =>t.ImportID==importId).ToList().FirstOrDefault();
            return result;
        }

        public async Task<List<ImportReceipts>> GetImportFindByDayAsync(DateTime fromDay, DateTime toDay)
        {
            var list = await _context.ImportReceipts.ToListAsync();
            var result = list.Where(t => t.ImportDate >= fromDay && t.ImportDate <=toDay).ToList();
            return result;
        }

        public async Task<List<ImportDetails>> importDetails(int importId)
        {
            var resutl = await _context.ImportDetails.Where(t => t.ImportID == importId).ToListAsync();
            return resutl;
        }
    }
}
