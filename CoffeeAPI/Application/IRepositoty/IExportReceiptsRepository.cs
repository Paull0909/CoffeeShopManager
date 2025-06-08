using Application.SeedWorks;
using Data.Entities;

namespace Application.IRepositoty
{
    public interface IExportReceiptsRepository : IRepository<ExportReceipts, int>
    {
        Task<List<ExportDetails>> GetExportDetails(int exportId);
        Task<List<ExportReceipts>> GetExportFindByDayAsync(DateTime fromDay, DateTime toDay);
    }
}
