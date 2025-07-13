using Application.IRepositoty;
using Application.Service;
using AutoMapper;
using Data.Context;
using Data.DTO.Report;
using Data.Entities;
using Data.Enum;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositoty
{
    class OrdersRepository : RepositoryBase<Orders, int>,IOrdersRepository
    {
        private readonly IMapper _mapper;
        public OrdersRepository(Web_Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Orders> BankTransferToCash(int id, TransactionStatus transactionStatus, bool orderStatus)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(t => t.OrderID == id);

            result.PaymentStatus= transactionStatus;
            result.OrderStatus = orderStatus;
            await _context.SaveChangesAsync();
            return result;

        }

        public async Task<bool> CheckCodeOrder(string code)
        {
            var result= await _context.Orders.FirstOrDefaultAsync(t=>t.CodeOrder==code);
            if (result != null) {
                return true;
            }
            return false;
        }

        public async Task<List<Orders>> GetAllOrdersByDay()
        {
            var orders = await _context.Orders.Where(t=>t.OrderDate.Day==DateTime.Today.Day&& t.OrderDate.Month==DateTime.Today.Month && t.OrderDate.Year==DateTime.Today.Year).ToListAsync();
            return orders;
        }

        public async Task<Orders> GetOrderByCodeOrder(string code)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(t => t.CodeOrder == code);
            return result;
        }

        public async Task<ReportRevenueByDay> GetReportRevenueByDayAsync()
        {
            var time = DateTime.Now;
            var startOfDay = time.Date;
            var orders = await _context.Orders.Where(x => x.OrderDate >= startOfDay && x.OrderDate <= time && x.OrderDate.Month == time.Month
                                                                                        && x.OrderDate.Year == time.Year).ToListAsync();
            var groupedOrders = orders.GroupBy(x => x.OrderDate.Hour)
                .Select(group => new
                {
                    Date = group.Key,
                    DailyRevenue = group.Sum(o => o.FinalAmount),
                    OrderCount = group.Count()
                })
                .ToList();

            ReportRevenueByDay result = new ReportRevenueByDay
            {
                Categories = new List<int>(), // Khởi tạo danh sách
                Data = new List<decimal>(),
                CountOrder=new List<int> ()
            };
            foreach (var order in groupedOrders)
            {
                result.Data.Add(order.DailyRevenue);
                result.Categories.Add(order.Date);
                result.CountOrder.Add(order.OrderCount);
            }

            return result;
        }

        public async Task<ReportRevenueByMonth> GetReportRevenueByMonthAsync()
        {
            var time = DateTime.Now;
            var orders = await _context.Orders.Where(x => x.OrderDate.Month == time.Month && x.OrderDate.Year == time.Year).ToListAsync();
            var groupedOrders = orders.GroupBy(x => x.OrderDate.Day)
                .Select(group => new
                {
                    Date = group.Key,
                    DailyRevenue = group.Sum(o => o.FinalAmount),

                })
                .ToList();

            ReportRevenueByMonth result = new ReportRevenueByMonth
            {
                Categories = new List<int>(), // Khởi tạo danh sách
                Data = new List<decimal>()
            };
            foreach (var order in groupedOrders)
            {
                result.Data.Add(order.DailyRevenue);
                result.Categories.Add(order.Date);
            }

            return result;
        }

        public async Task<ReportRevenueByYear> GetReportRevenueByYearAsync()
        {
            var time = DateTime.Now;
            var orders = await _context.Orders.Where(x => x.OrderDate.Year == time.Year).ToListAsync();
            var groupedOrders = orders.GroupBy(x => x.OrderDate.Month)
                .Select(group => new
                {
                    Date = group.Key,
                    DailyRevenue = group.Sum(o => o.FinalAmount),

                })
                .ToList();

            ReportRevenueByYear result = new ReportRevenueByYear
            {
                Categories = new List<int>(), // Khởi tạo danh sách
                Data = new List<decimal>()
            };
            foreach (var order in groupedOrders)
            {
                result.Data.Add(order.DailyRevenue);
                result.Categories.Add(order.Date);
            }

            return result;
        }

        public async Task<Orders> UpdateOrderByOrderStatus(int id, bool status)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(t => t.OrderID == id);

            result.OrderStatus = status;
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
