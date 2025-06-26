using Application.SeedWorks;
using AutoMapper;
using Data.DTO.Categories_Material;
using Data.DTO.Surcharges;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurchargesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public SurchargesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _unitOfWork.SurchargesRepository.GetAllAsync();
                return Ok(list);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetByDateStart_End")]
        public async Task<IActionResult> GetbyDate(DateTime start, DateTime end)
        {
            try
            {
                var list = _unitOfWork.SurchargesRepository.Find(x => x.StartDate <= end && x.EndDate >= start).ToList();
                return Ok(list);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("GetByToDay")]
        public async Task<IActionResult> GetbyToDay(DateTime date)
        {
            try
            {
                var list = _unitOfWork.SurchargesRepository.Find(x => x.StartDate <= date && x.EndDate >= date).FirstOrDefault();
                return Ok(list);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetbyName(string name)
        {
            try
            {
                var list = _unitOfWork.SurchargesRepository.Find(x => x.SurchargesName.Contains(name)).ToList();
                return Ok(list);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(SurchargesCreateUpdateRequest surcharges)
        {
            try
            {
                var su = _unitOfWork.SuppliersRepository.Find(x => x.SupplierName == surcharges.SurchargesName).ToList();
                if(su.Count()==0)
                {
                    var i = _mapper.Map<Surcharges>(surcharges);
                    _unitOfWork.SurchargesRepository.Add(i);
                    _unitOfWork.CompleteAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
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
                var cate = await _unitOfWork.SurchargesRepository.GetByIdAsync(id);
                _unitOfWork.SurchargesRepository.Remove(cate);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(SurchargesCreateUpdateRequest request)
        {
            try
            {
                var cate = await _unitOfWork.SurchargesRepository.GetByIdAsync(request.ID);
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
