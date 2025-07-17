using Application.IRepositoty;
using Application.Service;
using AutoMapper;
using Data.Context;
using Data.Entities;

namespace Application.Repositoty
{
    class ToppingsRepository : RepositoryBase<Toppings, int>,IToppingsRepository
    {
        private readonly IMapper _mapper;

        public ToppingsRepository(Web_Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public async Task<List<Toppings>> GetAllIsAvailable()
        {
            var list = _context.Toppings.Where(t => t.IsAvailable == true).ToList();
            return list;
        }
    }
}
