using Application.SeedWorks;
using AutoMapper;
using Data.DTO.Categories_Products;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Categories_ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public Categories_ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllCategories_Products")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var cate = await _unitOfWork.Categories_ProductsRepository.GetAllAsync();
                return Ok(cate);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("GetProductByCategories_ProductsID")]
        public async Task<IActionResult> GetProByID(int id)
        {
            try
            {
                var pr = _unitOfWork.ProductsRepository.Find(x=>x.CategoryID == id).ToList();
                return Ok(pr);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetAllCategories_ProductsByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var cate = _unitOfWork.Categories_ProductsRepository.Find(t=>t.CategoryName.Contains(name));
                return Ok(cate);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Categories_ProductsCreateUpdateRequest request)
        {
            try
            {
                var cate = _unitOfWork.Categories_ProductsRepository.Find(T=>T.CategoryName == request.CategoryName);
                if (cate.Count()==0)
                {
                    var i = _mapper.Map<Categories_Products>(request);
                    _unitOfWork.Categories_ProductsRepository.Add(i);
                    await _unitOfWork.CompleteAsync();
                    return Ok();
                }
                else
                    return BadRequest($"Tên loại '{request.CategoryName}' đã tồn tại.");
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
                var cate = await _unitOfWork.Categories_ProductsRepository.GetByIdAsync(id);
                _unitOfWork.Categories_ProductsRepository.Remove(cate);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Categories_ProductsCreateUpdateRequest request)
        {
            try
            {
                var cate = await _unitOfWork.Categories_ProductsRepository.GetByIdAsync(request.CategoryID);
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
