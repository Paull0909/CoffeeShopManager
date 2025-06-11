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
    class EmployeesRepository : RepositoryBase<Employees, int>,IEmployeesRepository
    {
        private readonly IMapper _mapper;

        public EmployeesRepository(Web_Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Employees> GetEmloyeesByPositonID(int positonID)
        {
            return await _context.Employees.FirstOrDefaultAsync(t=>t.PositionID==positonID);
        }
    }
}
