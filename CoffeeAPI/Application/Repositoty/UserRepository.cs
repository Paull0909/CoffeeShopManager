using Application.IRepositoty;
using Application.Service;
using AutoMapper;
using Data.Context;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositoty
{
    class UserRepository : RepositoryBase<User, int>,IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(Web_Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<int> CountRoleIdInUserRole(Guid roleId)
        {
            var count = _context.UserRoles.Where(x=>x.RoleId==roleId).Count();
            return count;
        }

        public async Task<User> GetUser(Guid id)
        {
            var user =  _context.Users.Find(id);
            
            return user;
        }
    }
}
