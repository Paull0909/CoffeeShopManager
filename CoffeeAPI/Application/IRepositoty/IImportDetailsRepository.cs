﻿using Application.SeedWorks;
using Data.Entities;

namespace Application.IRepositoty
{
    public interface IImportDetailsRepository : IRepository<ImportDetails, int>
    {
        Task<List<ImportDetails>> GetByImportReceipt(int id);
    }
}
