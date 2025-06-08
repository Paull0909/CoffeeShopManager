using Application.SeedWorks;
using Data.Entities;

namespace Application.IRepositoty
{
    public interface IMaterialsRepository : IRepository<Materials, int>
    {
        bool FindName(string name);
        Task<List<Materials>> GetByCategory(int id);
        Task<List<Materials>> GetBySuppliers(int id);
        Task<List<Lot>> GetByLotByMaterialsID(int id);
        Task<Lot> GetLotByID(int id);

        Task<List<Materials>> GetMaterialByFindName(string name);
        //Task<Materials> UpdateTotalMaterials(int id,int total);
    }
}
