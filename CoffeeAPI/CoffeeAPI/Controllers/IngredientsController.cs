using Application.SeedWorks;
using AutoMapper;
using Data.DTO.Ingredients;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public IngredientsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllIngredients")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var cate = await _unitOfWork.IngredientsRepository.GetAllAsync();
                return Ok(cate);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("GetAllIngredientsByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var cate = _unitOfWork.IngredientsRepository.Find(x=>x.Name.Contains(name)).ToList();
                return Ok(cate);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(IngredientsCreateUpdateRequest request)
        {
            try
            {
                var cate = _unitOfWork.IngredientsRepository.Find(t => t.Name == request.Name);
                if (cate.Count() == 0)
                {
                    var i = _mapper.Map<Ingredients>(request);
                    _unitOfWork.IngredientsRepository.Add(i);
                    await _unitOfWork.CompleteAsync();
                    return Ok();
                }
                else
                    return BadRequest($"Tên '{request.Name}' đã tồn tại.");
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
                var cate = await _unitOfWork.IngredientsRepository.GetByIdAsync(id);
                _unitOfWork.IngredientsRepository.Remove(cate);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(IngredientsCreateUpdateRequest request)
        {
            try
            {
                var cate = await _unitOfWork.IngredientsRepository.GetByIdAsync(request.Id);
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
