using Application.SeedWorks;
using Data.Entities;

namespace Application.IRepositoty
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User> GetUser(Guid id);
        Task<int> CountRoleIdInUserRole(Guid roleId);

        Task<int> CountUserIdInUserRole(Guid userId);
    }
}
