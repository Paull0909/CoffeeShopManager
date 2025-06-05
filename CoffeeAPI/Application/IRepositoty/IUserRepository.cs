using Application.SeedWorks;
using Data.Entities;

namespace Application.IRepositoty
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User> GetUser(Guid id);
    }
}
