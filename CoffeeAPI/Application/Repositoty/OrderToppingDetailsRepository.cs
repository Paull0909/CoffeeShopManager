using Application.IRepositoty;
using Application.Service;
using AutoMapper;
using Data.Context;
using Data.Entities;

namespace Application.Repositoty
{
    public class OrderToppingDetailsRepository : RepositoryBase<OrderToppingDetails, int>, IOrderToppingDetailsRepository
    {
        private readonly IMapper _mapper;

        public OrderToppingDetailsRepository(Web_Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
    }
}
