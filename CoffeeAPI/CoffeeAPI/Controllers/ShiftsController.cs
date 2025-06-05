using Application.SeedWorks;
using AutoMapper;
using Data.DTO.Positions;
using Data.DTO.Shifts;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ShiftsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllShifts")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var cate = await _unitOfWork.ShiftsRepository.GetAllAsync();
                return Ok(cate);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetShiftsByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var shifts = _unitOfWork.ShiftsRepository
                    .Find(x => x.ShiftName.Contains(name))
                    ?.ToList();

                return Ok(shifts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShiftsCreateUpdateRequest request)
        {
            try
            {
                var sh = _unitOfWork.ShiftsRepository.Find(t => t.ShiftName == request.ShiftName);
                if (sh.Count() == 0)
                {
                    var i = _mapper.Map<Shifts>(request);
                    _unitOfWork.ShiftsRepository.Add(i);
                    await _unitOfWork.CompleteAsync();
                    return Ok();
                }
                else return BadRequest();
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
                var ep = await _unitOfWork.ShiftsRepository.GetByIdAsync(id);
                _unitOfWork.ShiftsRepository.Remove(ep);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest("Ca lam da ton tai");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ShiftsCreateUpdateRequest request)
        {
            try
            {
                var cate = await _unitOfWork.ShiftsRepository.GetByIdAsync(request.ShiftID);
                if (cate != null)
                {
                    var i = _mapper.Map(request,cate);
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
