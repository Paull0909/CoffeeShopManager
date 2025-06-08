using Application.SeedWorks;
using AutoMapper;
using Data.DTO.Positions;
using Data.DTO.Toppings;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToppingsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ToppingsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllToppings")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tp = await _unitOfWork.ToppingsRepository.GetAllAsync();
                return Ok(tp);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetToppingsByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var tp = _unitOfWork.ToppingsRepository.Find(x=>x.ToppingName.Contains(name)).ToList();
                return Ok(tp);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ToppingsCreateUpdateRequest request)
        {
            try
            {
                var tp = _unitOfWork.ToppingsRepository.Find(t => t.ToppingName == request.ToppingName);
                if (tp.Count() == 0)
                {
                    var i = _mapper.Map<Toppings>(request);
                    _unitOfWork.ToppingsRepository.Add(i);
                    await _unitOfWork.CompleteAsync();
                    return Ok();
                }
                return BadRequest("Ten topping da ton tai");
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
                var tp = await _unitOfWork.ToppingsRepository.GetByIdAsync(id);
                _unitOfWork.ToppingsRepository.Remove(tp);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ToppingsCreateUpdateRequest request)
        {
            try
            {
                var cate = await _unitOfWork.ToppingsRepository.GetByIdAsync(request.ToppingID);
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
