using Application.SeedWorks;
using AutoMapper;
using Data.DTO.Employees;
using Data.DTO.EmployeeSchedules;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeSchedulesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeSchedulesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllEmployeeSchedules")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var eps = await _unitOfWork.EmployeeSchedulesRepository.GetAllAsync();
                var list = new List<EmployeeSchedulesViewModel>();
                foreach(var item in eps)
                {
                    var i = _mapper.Map<EmployeeSchedulesViewModel>(item);
                    var ep = await _unitOfWork.EmployeesRepository.GetByIdAsync(item.EmployeeID);
                    var sh = await _unitOfWork.ShiftsRepository.GetByIdAsync(item.ShiftID);
                    i.FullName = ep.FullName;
                    i.ShiftName = sh.ShiftName;
                    list.Add(i);
                }
                return Ok(list);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetEmployeeSchedulesByFilter")]
        public async Task<IActionResult> GetByFilter(DateOnly start,  DateOnly end)
        {
            try
            {
                var eps = _unitOfWork.EmployeeSchedulesRepository.Find(x=>x.WorkDate >= start && x.WorkDate <= end).ToList();
                var list = new List<EmployeeSchedulesViewModel>();
                foreach (var item in eps)
                {
                    var i = _mapper.Map<EmployeeSchedulesViewModel>(item);
                    var ep = await _unitOfWork.EmployeesRepository.GetByIdAsync(item.EmployeeID);
                    var sh = await _unitOfWork.ShiftsRepository.GetByIdAsync(item.ShiftID);
                    i.FullName = ep.FullName;
                    i.ShiftName = sh.ShiftName;
                    list.Add(i);
                }
                return Ok(list);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeSchedulesCreateUpdateRequest request)
        {
            try
            {
                var i = _mapper.Map<EmployeeSchedules>(request);
                _unitOfWork.EmployeeSchedulesRepository.Add(i);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ep = await _unitOfWork.EmployeeSchedulesRepository.GetByIdAsync(id);
                _unitOfWork.EmployeeSchedulesRepository.Remove(ep);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(EmployeeSchedulesCreateUpdateRequest request)
        {
            try
            {
                var cate = await _unitOfWork.EmployeeSchedulesRepository.GetByIdAsync(request.ScheduleID);
                if (cate != null)
                {
                    var i = _mapper.Map(request, cate);
                    await _unitOfWork.CompleteAsync();
                    return Ok();
                }
                else
                    return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
