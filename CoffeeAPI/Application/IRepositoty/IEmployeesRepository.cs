using Application.SeedWorks;
using Data.Entities;

namespace Application.IRepositoty
{
    public interface IEmployeesRepository : IRepository<Employees, int>
    {
        Task<Employees> GetEmloyeesByPositonID(int positonID);
    }
}
