using Application.SeedWorks;
using Data.Entities;

namespace Application.IRepositoty
{
    public interface ICategories_MaterialRepository: IRepository<Categories_Material, int>
    {
        Task<List<Categories_Material>> GetCategoryMaterialByFindName(string name);
    }
}
