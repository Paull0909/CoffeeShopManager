using Application.IRepositoty;
using Application.Service;
using AutoMapper;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositoty
{
    public class OrderToppingDetailsRepository : RepositoryBase<OrderToppingDetails, int>, IOrderToppingDetailsRepository
    {
        private readonly IMapper _mapper;

        public OrderToppingDetailsRepository(Web_Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<OrderToppingDetails>> GetByOrderDetailsID(int orderdetailID)
        {
            var list = await _context.OrderToppingsDetails.Where(t => t.OrderDetailID == orderdetailID).ToListAsync();
            return list;
        }
    }
}
