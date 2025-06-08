using Application.SeedWorks;
using Data.Entities;

namespace Application.IRepositoty
{
    public interface ISuppliersRepository : IRepository<Suppliers, int>
    {
        bool FindName(string name);
        Task<Suppliers> GetSuppliersByMaterialsID(int materialID);
        Task<List<Suppliers>> GetSuppliersByFindName(string name);
    }
}
