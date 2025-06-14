using Application.IRepositoty;
using Application.Service;
using AutoMapper;
using Data.Context;
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
            var orders = await _context.Orders.Where(t=>t.OrderDate.Day==DateTime.Today.Day).ToListAsync();
            return orders;
        }

        public async Task<Orders> GetOrderByCodeOrder(string code)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(t => t.CodeOrder == code);
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
