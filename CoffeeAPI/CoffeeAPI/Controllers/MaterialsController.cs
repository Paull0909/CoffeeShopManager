using Application.SeedWorks;
using AutoMapper;
using Data.DTO.Materials;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public MaterialsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var cate = await _unitOfWork.MaterialsRepository.GetAllAsync();
                return Ok(cate);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetAllLot")]
        public async Task<IActionResult> GetLot()
        {
            try
            {
                var cate = await _unitOfWork.LotRepository.GetAllAsync();
                return Ok(cate);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetLotById")]
        public async Task<IActionResult> GetLotById(int id)
        {
            try
            {
                var lot = await _unitOfWork.LotRepository.GetByIdAsync(id);
                return Ok(lot);
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
                var cate = await _unitOfWork.MaterialsRepository.GetByCategory(id);
                return Ok(cate);
            }
            catch
            {
                return BadRequest();
            }
        }



        [HttpGet("GetBySuppliers")]
        public async Task<IActionResult> GetBySuppliers(int id)
        {
            try
            {
                var cate = await _unitOfWork.MaterialsRepository.GetBySuppliers(id);
                return Ok(cate);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetByLot")]
        public async Task<IActionResult> GetLotByMaterialID(int id)
        {
            try
            {
                var lot = await _unitOfWork.MaterialsRepository.GetByLotByMaterialsID(id);
                return Ok(lot);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetByID")]
        public async Task<IActionResult> GetByID(int id)
        {
            try
            {
                var materials = await _unitOfWork.MaterialsRepository.GetByIdAsync(id);
                return Ok(materials);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetAllLotDetail")]
        public async Task<IActionResult> GetAllLotDetail()
        {
            try
            {
                var list = new List<Data.DTO.Materials.InventoryLogs>();
                var lotdetais = await _unitOfWork.LotDeatailsRepository.GetAllAsync();
                foreach (var lotDetail in lotdetais)
                {
                    var lot = await _unitOfWork.LotRepository.GetByIdAsync(lotDetail.LotId);
                    var material= await _unitOfWork.MaterialsRepository.GetByIdAsync(lot.MaterialID);

                    var inven = new Data.DTO.Materials.InventoryLogs()
                    {
                        MaterialID = material.MaterialID,
                        CreateAt=lotDetail.CreateAt,
                        LotID=lotDetail.LotId,
                        MaterialName = material.MaterialName,
                        Quantity =lotDetail.Quantity,
                        QuantityAfter =lotDetail.QuantityAfter,
                        QuantityBefor =lotDetail.QuantityBefor,
                        Status=lotDetail.Status,
                    };
                    list.Add(inven);

                }
                return Ok(list);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(MaterialsCreateUpdateRequest request)
        {
            try
            {
                var mate = _unitOfWork.MaterialsRepository.Find(t => t.MaterialName == request.MaterialName);
                if (mate.Count() == 0)
                {
                    var i = _mapper.Map<Materials>(request);
                    _unitOfWork.MaterialsRepository.Add(i);
                    await _unitOfWork.CompleteAsync();
                    return Ok();
                }
                else
                    return BadRequest($"Tên loại '{request.MaterialName}' đã tồn tại.");
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
                var mate = await _unitOfWork.MaterialsRepository.GetByIdAsync(id);
                _unitOfWork.MaterialsRepository.Remove(mate);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(MaterialsCreateUpdateRequest request)
        {
            try
            {
                var mate = await _unitOfWork.MaterialsRepository.GetByIdAsync(request.MaterialID);
                if (mate != null)
                {
                    var i = _mapper.Map(request, mate);
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

        [HttpGet("GetMaterialByName")]
        public async Task<IActionResult> GetMaterialByName(string name)
        {
            try
            {
                var cate = await _unitOfWork.MaterialsRepository.GetMaterialByFindName(name);
                return Ok(cate);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetLotDetailsByDay")]
        public async Task<IActionResult> GetLotDetailsByDay(DateTime fromDay, DateTime toDay)
        {
            try
            {
                var list = new List<Data.DTO.Materials.InventoryLogs>();
                var lots = await _unitOfWork.LotDeatailsRepository.GetLotDetailsFindByDayAsync(fromDay, toDay);
                foreach (var lotDetail in lots)
                {
                    var lot = await _unitOfWork.LotRepository.GetByIdAsync(lotDetail.LotId);
                    var material = await _unitOfWork.MaterialsRepository.GetByIdAsync(lot.MaterialID);

                    var inven = new Data.DTO.Materials.InventoryLogs()
                    {
                        MaterialID = material.MaterialID,
                        CreateAt = lotDetail.CreateAt,
                        LotID = lotDetail.LotId,
                        MaterialName = material.MaterialName,
                        Quantity = lotDetail.Quantity,
                        QuantityAfter = lotDetail.QuantityAfter,
                        QuantityBefor = lotDetail.QuantityBefor,
                        Status = lotDetail.Status,
                    };
                    list.Add(inven);

                }
                return Ok(list);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
