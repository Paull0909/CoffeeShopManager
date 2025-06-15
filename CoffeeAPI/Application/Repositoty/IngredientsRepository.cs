using Application.IRepositoty;
using Application.Service;
using AutoMapper;
using Data.Context;
using Data.Entities;

namespace Application.Repositoty
{
    class IngredientsRepository : RepositoryBase<Ingredients, int>, IIngredientsRepository
    {
        private readonly IMapper _mapper;
        public IngredientsRepository(Web_Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
    }
}
