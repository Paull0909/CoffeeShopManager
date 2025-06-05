using AutoMapper;

namespace Data.DTO.Timekeeping
{
    public class TimekeepingViewModel
    {
        public int TimekeepingID { get; set; }
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public DateOnly WorkDate { get; set; }
        public TimeOnly CheckInTime { get; set; }
        public TimeOnly CheckOutTime { get; set; }
        public float WorkingHours { get; set; }
        public string Note { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.Timekeeping, TimekeepingViewModel>();
            }
        }
    }
}
