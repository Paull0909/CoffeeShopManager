using Application.SeedWorks;
using Data.DTO.ImportDetails;
using Data.Entities;

namespace Application.IRepositoty
{
    public interface IImportReceiptsRepository : IRepository<ImportReceipts, int>
    {
        Task<List<ImportDetails>> importDetails(int importId);
        Task<ImportReceipts> GetImportByFindIdAsync(int importId);
        Task<List<ImportReceipts>> GetImportFindByDayAsync(DateTime fromDay,DateTime toDay);
    }
}
