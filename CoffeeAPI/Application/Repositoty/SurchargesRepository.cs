using Application.IRepositoty;
using Application.Service;
using AutoMapper;
using Data.Context;
using Data.Entities;

namespace Application.Repositoty
{
    class SurchargesRepository : RepositoryBase<Surcharges,int>,ISurchargesRepository
    {
        private readonly IMapper _mapper;

        public SurchargesRepository(Web_Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
    }
}
