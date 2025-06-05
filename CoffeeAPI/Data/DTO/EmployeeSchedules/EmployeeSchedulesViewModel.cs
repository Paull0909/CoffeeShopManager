using AutoMapper;

namespace Data.DTO.EmployeeSchedules
{
    public class EmployeeSchedulesViewModel
    {
        public int ScheduleID { get; set; }
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public int ShiftID { get; set; }
        public string ShiftName { get; set; }
        public DateOnly WorkDate { get; set; }
        public bool IsWorking { get; set; }
        public string Note { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.EmployeeSchedules, EmployeeSchedulesViewModel>();
            }
        }
    }
}
