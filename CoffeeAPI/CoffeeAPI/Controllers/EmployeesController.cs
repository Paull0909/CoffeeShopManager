using Application.SeedWorks;
using AutoMapper;
using Data.DTO.Categories_Material;
using Data.DTO.Employees;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EmployeesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var cate = await _unitOfWork.EmployeesRepository.GetAllAsync();
                var list = new List<EmployeesViewModel>();
                foreach(var item in cate)
                {
                    var ep = _mapper.Map<EmployeesViewModel>(item);
                    var i = await _unitOfWork.PositionsRepository.GetByIdAsync(item.PositionID);
                    ep.PositionName = i?.PositionName;
                    list.Add(ep);
                }               
                return Ok(list);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeesCreateUpdateRequest request)
        {
            try
            {
                var i = _mapper.Map<Employees>(request);
                _unitOfWork.EmployeesRepository.Add(i);
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
                var ep = await _unitOfWork.EmployeesRepository.GetByIdAsync(id);
                _unitOfWork.EmployeesRepository.Remove(ep);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(EmployeesCreateUpdateRequest request)
        {
            try
            {
                var cate = await _unitOfWork.EmployeesRepository.GetByIdAsync(request.EmployeeID);
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

        [HttpGet("GetEmployeesByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var employees = _unitOfWork.EmployeesRepository.Find(e => e.FullName.Contains(name))?.ToList();
                if (employees == null || !employees.Any())
                    return NotFound("Không tìm thấy nhân viên nào.");

                var result = new List<EmployeesViewModel>();
                foreach (var emp in employees)
                {
                    var vm = _mapper.Map<EmployeesViewModel>(emp);

                    var pos = await _unitOfWork.PositionsRepository.GetByIdAsync(emp.PositionID);
                    vm.PositionName = pos?.PositionName ?? "(Không rõ)";

                    result.Add(vm);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("GetEmployeByID")]
        public async Task<IActionResult> GetEmployByPositionID(int id)
        {
            try
            {
                var result= await _unitOfWork.EmployeesRepository.GetEmloyeesByPositonID(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
