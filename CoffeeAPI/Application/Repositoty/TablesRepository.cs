using Application.IRepositoty;
using Application.Service;
using AutoMapper;
using Data.Context;
using Data.Entities;
using Data.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositoty
{
    class TablesRepository : RepositoryBase<Tables, int>,ITablesRepository
    {
        private readonly IMapper _mapper;

        public TablesRepository(Web_Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<Tables>> GetOrderNumberTagByStatus(TableStatus status)
        {
            var result = await _context.Tables.Where(t => t.Status == status).ToListAsync();
            return result;
        }

        public async Task<Tables> updateStatus(int id, TableStatus status)
        {
            var result= await _context.Tables.FirstOrDefaultAsync(t=>t.TableID==id);

            result.Status = status;
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
