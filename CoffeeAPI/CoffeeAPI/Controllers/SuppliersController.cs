using Application.SeedWorks;
using AutoMapper;
using Data.DTO.Suppliers;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public SuppliersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllSuppliers")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var il = await _unitOfWork.SuppliersRepository.GetAllAsync();
                return Ok(il);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetSuppliersByID")]
        public async Task<IActionResult> GetSuppliersID(int id)
        {
            try
            {
                var il = await _unitOfWork.SuppliersRepository.GetByIdAsync(id);
                return Ok(il);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(SuppliersCreateUpdateRequest request)
        {
            try
            {
                var sup = _unitOfWork.SuppliersRepository.Find(t=>t.SupplierName == request.SupplierName);
                if(sup.Count()==0)
                {
                    var i = _mapper.Map<Suppliers>(request);
                    _unitOfWork.SuppliersRepository.Add(i);
                    await _unitOfWork.CompleteAsync();
                    return Ok();
                }
               else return BadRequest($"Tên nha cung cap '{request.SupplierName}' đã tồn tại.");
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
                var il = await _unitOfWork.SuppliersRepository.GetByIdAsync(id);
                _unitOfWork.SuppliersRepository.Remove(il);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(SuppliersCreateUpdateRequest request)
        {
            try
            {
                var ex = await _unitOfWork.SuppliersRepository.GetByIdAsync(request.SupplierID);
                if (ex != null)
                {
                    var i = _mapper.Map(request,ex);
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
