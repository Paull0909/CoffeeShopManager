using Application.SeedWorks;
using Data.Entities;

namespace Application.IRepositoty
{
    public interface IProductsRepository : IRepository<Products, int>
    {
        bool FindName(string name);
        Task<List<Products>> GetByCategory(int id);
        Task<List<Products>> GetAllIsAvailable();
    }
}
