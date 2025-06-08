using Application.SeedWorks;
using AutoMapper;
using Data.DTO.EmployeeSchedules;
using Data.DTO.Tables;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderNumberTagController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public OrderNumberTagController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllOrderNumberTag")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var eps = await _unitOfWork.TablesRepository.GetAllAsync();
                return Ok(eps);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetOrderNumberTagByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var eps = _unitOfWork.TablesRepository.Find(x=>x.TableName.Contains(name)).ToList();
                return Ok(eps);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(TablesCreateUpdateRequest request)
        {
            try
            {
                var tb = _unitOfWork.TablesRepository.Find(t => t.TableName == request.TableName);
                if (tb.Count() == 0)
                {
                    var i = _mapper.Map<Tables>(request);
                    _unitOfWork.TablesRepository.Add(i);
                    await _unitOfWork.CompleteAsync();
                    return Ok();
                }
                else return NotFound();
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
                var tb = await _unitOfWork.TablesRepository.GetByIdAsync(id);
                _unitOfWork.TablesRepository.Remove(tb);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(TablesCreateUpdateRequest request)
        {
            try
            {
                var cate = await _unitOfWork.TablesRepository.GetByIdAsync(request.TableID);
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
