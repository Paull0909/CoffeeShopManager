using Application.SeedWorks;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositoty
{
    public interface ILotRepository : IRepository<Lot, int>
    {
    }
}
