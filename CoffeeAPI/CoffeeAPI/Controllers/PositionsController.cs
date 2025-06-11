using Application.SeedWorks;
using AutoMapper;
using Data.DTO.Employees;
using Data.DTO.Positions;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public PositionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllPositions")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var cate = await _unitOfWork.PositionsRepository.GetAllAsync();
                var list = new List<PositionsViewModel>();
                foreach(var item in cate)
                {
                    var i = _mapper.Map<PositionsViewModel>(item);
                    var ac = await _unitOfWork.UserRepository.GetUser(item.UserID);
                    i.UserName = ac.UserName;
                    list.Add(i);
                }
                return Ok(list);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetPositionsByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var positions = _unitOfWork.PositionsRepository
                    .Find(x => x.PositionName.Contains(name))
                    ?.ToList();

                if (positions == null || !positions.Any())
                    return NotFound("Không tìm thấy vị trí phù hợp.");

                var result = new List<PositionsViewModel>();

                foreach (var item in positions)
                {
                    var vm = _mapper.Map<PositionsViewModel>(item);

                    var user = await _unitOfWork.UserRepository.GetUser(item.UserID);
                    vm.UserName = user?.UserName ?? "(Không rõ)";

                    result.Add(vm);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(PositionsCreateUpdateRequest request)
        {
            try
            {
                var ps = _unitOfWork.PositionsRepository.Find(t => t.PositionName == request.PositionName);
                if (ps.Count() == 0)
                {
                    var i = _mapper.Map<Positions>(request);
                    _unitOfWork.PositionsRepository.Add(i);
                    await _unitOfWork.CompleteAsync();
                    return Ok();
                }
                else return BadRequest("Chuc vu da ton tai trong he thong");
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
                var ep = await _unitOfWork.PositionsRepository.GetByIdAsync(id);
                _unitOfWork.PositionsRepository.Remove(ep);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(PositionsCreateUpdateRequest request)
        {
            try
            {
                var cate = await _unitOfWork.PositionsRepository.GetByIdAsync(request.PositionID);
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


        [HttpGet("GetPositionsByUserId")]
        public async Task<IActionResult> GetPositionByUserId(Guid userId)
        {
            try
            {
                var result = await _unitOfWork.PositionsRepository.GetPositionByUserIdAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
