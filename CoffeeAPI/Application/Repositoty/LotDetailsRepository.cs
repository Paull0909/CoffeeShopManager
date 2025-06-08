using Application.IRepositoty;
using Application.Service;
using AutoMapper;
using Data.Context;
using Data.Entities;

namespace Application.Repositoty
{
    public class LotDetailsRepository : RepositoryBase<LotDetails, int>, ILotDeatailsRepository
    {
        private readonly IMapper _mapper;

        public LotDetailsRepository(Web_Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
    }
}
