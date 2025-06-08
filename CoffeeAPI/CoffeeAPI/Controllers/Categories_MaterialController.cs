using Application.SeedWorks;
using AutoMapper;
using Data.DTO.Categories_Material;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Categories_MaterialController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public Categories_MaterialController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var cate = await _unitOfWork.Categories_MaterialRepository.GetAllAsync();
                return Ok(cate);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetAllCategories_MaterialByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var cate = _unitOfWork.Categories_MaterialRepository.Find(t => t.CategoryName.Contains(name));
                return Ok(cate);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Categories_MaterialCreateUpdateRequest request)
        {
            try
            {
                var cate = _unitOfWork.Categories_MaterialRepository.Find(t=>t.CategoryName == request.CategoryName);
                if(cate.Count() == 0)
                {
                    var i = _mapper.Map<Categories_Material>(request);
                    _unitOfWork.Categories_MaterialRepository.Add(i);
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
                var cate = await _unitOfWork.Categories_MaterialRepository.GetByIdAsync(id);
                _unitOfWork.Categories_MaterialRepository.Remove(cate);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Categories_MaterialCreateUpdateRequest request)
        {
            try
            {
                var cate = await _unitOfWork.Categories_MaterialRepository.GetByIdAsync(request.CategoryID);
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

        [HttpGet("GetCategoriesMaterialByName")]
        public async Task<IActionResult> GetCategoriesMaterialByName(string name)
        {
            try
            {
                var cate = await _unitOfWork.Categories_MaterialRepository.GetCategoryMaterialByFindName(name);
                return Ok(cate);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
