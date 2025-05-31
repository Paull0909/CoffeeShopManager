using AutoMapper;

namespace Data.DTO.Salaries
{
    public class SalariesCreateUpdateRequest
    {
        public int SalaryID { get; set; }
        public int EmployeeID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public float TotalWorkingHours { get; set; }
        public float Bonus { get; set; }
        public decimal Penalty { get; set; }
        public decimal FinalSalary { get; set; }
        public DateOnly CreatedAt { get; set; }
        public Guid UserID { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<SalariesCreateUpdateRequest, Entities.Salaries>();
            }
        }
    }
}
