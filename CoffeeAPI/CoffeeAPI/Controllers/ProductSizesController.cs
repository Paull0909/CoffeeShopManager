using Application.SeedWorks;
using AutoMapper;
using Data.DTO.Employees;
using Data.DTO.Positions;
using Data.DTO.Products;
using Data.DTO.ProductSizes;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSizesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductSizesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllProductSizes")]
        public async Task<IActionResult> GetByProduct(int id)
        {
            try
            {
                var size = await _unitOfWork.ProductSizesRepository.GetByProduct(id);
                var list = new List<ProductSizesViewModel>();
                foreach (var item in size)
                {
                    var ep = _mapper.Map<ProductSizesViewModel>(item);
                    var i = await _unitOfWork.ProductsRepository.GetByIdAsync(item.ProductID);
                    ep.ProductName = i?.ProductName;
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
        public async Task<IActionResult> Create(ProductSizesCreateUpdateRequest request)
        {
            try
            {
                var size = _unitOfWork.ProductSizesRepository.Find(t => t.ProductID == request.ProductID && t.SizeName == request.SizeName);
                if (size.Count() == 0)
                {
                    var i = _mapper.Map<ProductSizes>(request);
                    _unitOfWork.ProductSizesRepository.Add(i);
                    await _unitOfWork.CompleteAsync();
                    return Ok();
                }
                else return BadRequest("Size la ton tai trong san pham");
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
                var size = await _unitOfWork.ProductSizesRepository.GetByIdAsync(id);
                _unitOfWork.ProductSizesRepository.Remove(size);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductSizesCreateUpdateRequest request)
        {
            try
            {
                var cate = await _unitOfWork.ProductSizesRepository.GetByIdAsync(request.ProductSizeID);
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
