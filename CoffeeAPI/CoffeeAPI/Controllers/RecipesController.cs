using Application.SeedWorks;
using AutoMapper;
using Azure.Core;
using Data.DTO.Positions;
using Data.DTO.Recipes;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public RecipesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllRecipes")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var rc = await _unitOfWork.RecipesRepository.GetAllAsync();
                return Ok(rc);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetRecipesByProduct")]
        public async Task<IActionResult> GetByProduct(int sizeID)
        {
            try
            {
                var rc = _unitOfWork.RecipesRepository.Find(x=>x.ProductSizeID == sizeID).ToList();
                return Ok(rc);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecipesCreateUpdateRequest request)
        {
            try
            {
                
                var i = _mapper.Map<Recipes>(request);
                _unitOfWork.RecipesRepository.Add(i);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("CheckWhenAdd")]
        public async Task<IActionResult> CheckAdd(int size, int ingre)
        {
            try
            {
                var check = _unitOfWork.RecipesRepository.Find(x => x.ProductSizeID == size && x.IngredientsID == ingre).ToList();
                bool result = false;
                if (check.Count > 0)
                {
                    result = true;
                    return Ok(result);
                }
                else
                {
                    return Ok(result);
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
                var rc = await _unitOfWork.RecipesRepository.GetByIdAsync(id);
                _unitOfWork.RecipesRepository.Remove(rc);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(RecipesCreateUpdateRequest request)
        {
            try
            {
                var rc = await _unitOfWork.RecipesRepository.GetByIdAsync(request.RecipeID);
                if (rc != null)
                {
                    var i = _mapper.Map(request,rc);
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
