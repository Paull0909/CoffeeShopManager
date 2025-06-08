using Application.SeedWorks;
using AutoMapper;
using Data.DTO.Employees;
using Data.DTO.Materials;
using Data.DTO.Products;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var pr = await _unitOfWork.ProductsRepository.GetAllAsync();
                var list = new List<ProductsViewModel>();
                foreach(var item in pr)
                {
                    var i = _mapper.Map<ProductsViewModel>(item);
                    var j = await _unitOfWork.Categories_ProductsRepository.GetByIdAsync(i.CategoryID);
                    i.Category_Name = j?.CategoryName;
                    list.Add(i);
                }
                return Ok(list);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetProductsByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var pr = _unitOfWork.ProductsRepository.Find(e => e.ProductName.Contains(name))?.ToList();
                if (pr == null || !pr.Any())
                    return NotFound("Không tìm thấy san pham nào.");

                var result = new List<ProductsViewModel>();
                foreach (var item in pr)
                {
                    var vm = _mapper.Map<ProductsViewModel>(item);

                    var cate = await _unitOfWork.Categories_ProductsRepository.GetByIdAsync(item.CategoryID);
                    vm.Category_Name = cate?.CategoryName ?? "(Không rõ)";

                    result.Add(vm);
                }

                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetProductsByNameandCate")]
        public async Task<IActionResult> GetByNameandCate(string name, int id)
        {
            try
            {
                var pr = await _unitOfWork.ProductsRepository.GetByCategory(id);
                if (pr == null || !pr.Any())
                    return NotFound("Không tìm thấy san pham nào.");
                var result = new List<ProductsViewModel>();
                var list = pr.Where(x => x.ProductName.Contains(name)).ToList();
                foreach (var item in list)
                {
                    var vm = _mapper.Map<ProductsViewModel>(item);

                    var cate = await _unitOfWork.Categories_ProductsRepository.GetByIdAsync(item.CategoryID);
                    vm.Category_Name = cate?.CategoryName ?? "(Không rõ)";

                    result.Add(vm);
                }

                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("GetByCategory")]
        public async Task<IActionResult> GetByCategory(int id)
        {
            try
            {
                var cate = await _unitOfWork.ProductsRepository.GetByCategory(id);
                return Ok(cate);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductsCreateUpdateRequest request)
        {
            try
            {
                var pr = _unitOfWork.ProductsRepository.Find(t=>t.ProductName == request.ProductName);
                if (pr.Count() == 0)
                {
                    var i = _mapper.Map<Products>(request);
                    _unitOfWork.ProductsRepository.Add(i);
                    await _unitOfWork.CompleteAsync();
                    return Ok();
                }
                else
                    return BadRequest($"Tên mon '{request.ProductName}' đã tồn tại.");
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
                var pr = await _unitOfWork.ProductsRepository.GetByIdAsync(id);
                _unitOfWork.ProductsRepository.Remove(pr);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductsCreateUpdateRequest request)
        {
            try
            {
                var mate = await _unitOfWork.ProductsRepository.GetByIdAsync(request.ProductID);
                if (mate != null)
                {
                    var i = _mapper.Map(request,mate);
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
