using Application.SeedWorks;
using Data.DTO.Report;
using Data.Entities;
using Data.Enum;

namespace Application.IRepositoty
{
    public interface IOrdersRepository : IRepository<Orders, int>
    {
        Task<bool> CheckCodeOrder(string code);
        Task<Orders> GetOrderByCodeOrder(string code);
        Task<Orders> UpdateOrderByOrderStatus(int id, bool status);
        Task<List<Orders>> GetAllOrdersByDay();
        Task<Orders> BankTransferToCash (int id,TransactionStatus transactionStatus,bool orderStatus);
        Task<ReportRevenueByMonth> GetReportRevenueByMonthAsync();
        Task<ReportRevenueByYear> GetReportRevenueByYearAsync();
        Task<ReportRevenueByDay> GetReportRevenueByDayAsync();
    }
}
