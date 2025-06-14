using Application.SeedWorks;
using Data.Entities;

namespace Application.IRepositoty
{
    public interface IOrderToppingDetailsRepository : IRepository<OrderToppingDetails, int>
    {
        Task<List<OrderToppingDetails>> GetByOrderDetailsID(int orderdetailID);
    }
}
