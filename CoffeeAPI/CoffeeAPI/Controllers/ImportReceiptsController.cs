using Application.SeedWorks;
using AutoMapper;
using Data.DTO.ImportDetails;
using Data.DTO.ImportReceipts;
using Data.DTO.Lot;
using Data.DTO.LotDetails;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportReceiptsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ImportReceiptsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllImport")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var ip = await _unitOfWork.ImportReceiptsRepository.GetAllAsync();
                return Ok(ip);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetImportDetailsByImportID")]
        public async Task<IActionResult> GetImportDetailsByImportID(int ImportID)
        {
            try
            {
                var ip = await _unitOfWork.ImportReceiptsRepository.importDetails(ImportID);
                return Ok(ip);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateImportReceipts request)
        {
            try
            {
                var i = _mapper.Map<ImportReceipts>(request.Receipt);
                _unitOfWork.ImportReceiptsRepository.Add(i);
                var resutl = await _unitOfWork.CompleteAsync();
                if (resutl > 0)
                {
                    foreach (var item in request.Details)
                    {
                        var materials = await _unitOfWork.MaterialsRepository.GetByIdAsync(item.MaterialID);
                        var id = i.ImportID;
                        item.ImportID = id;
                        var total = item.Price * item.Quantity;
                        item.TotalPrice = total;
                        var improt = _mapper.Map<ImportDetailsCreateUpdateRequest, ImportDetails>(item);
                        _unitOfWork.ImportDetailsRepository.Add(improt);

                        var lot = new LotCreateUpdateRequest()
                        {
                            MaterialID = improt.MaterialID,
                            PurchasePrice = improt.Price,
                            Quantity = improt.Quantity,
                            Status = "Cong So Luong",
                            ExpirationDate = improt.ExpirationDate,
                        };
                        lot.LotName = materials.MaterialName + " (" + lot.ExpirationDate + ")";
                        var lotresult = _mapper.Map<LotCreateUpdateRequest, Lot>(lot);
                        _unitOfWork.LotRepository.Add(lotresult);
                        await _unitOfWork.CompleteAsync(); 
                        var lotDetail = new LotDetailsCreateUpdateRequets()
                        {
                            LotId = lotresult.LotID,
                            Quantity = improt.Quantity,
                            QuantityBefor=0,
                            CreateAt = DateTime.Now,
                            Status = "Cong so luong"
                        };
                        lotDetail.QuantityAfter=lotDetail.QuantityBefor+lotDetail.Quantity;
                        var lotdetailsresult= _mapper.Map<LotDetailsCreateUpdateRequets,LotDetails>(lotDetail);
                        _unitOfWork.LotDeatailsRepository.Add(lotdetailsresult);
                        materials.TotalMaterial += item.Quantity;
                        await _unitOfWork.CompleteAsync();
                    }

                }

                return Ok();
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
                var ip = await _unitOfWork.ImportReceiptsRepository.GetByIdAsync(id);
                _unitOfWork.ImportReceiptsRepository.Remove(ip);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ImportReceiptsCreateUpdateRequest request)
        {
            try
            {
                var ex = await _unitOfWork.ImportReceiptsRepository.GetByIdAsync(request.ImportID);
                if (ex != null)
                {
                    var i = _mapper.Map(request, ex);
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

        [HttpGet("GetImportReceiptsById")]
        public async Task<IActionResult> GetImportReceiptsById(int id)
        {
            try
            {
                var import = await _unitOfWork.ImportReceiptsRepository.GetImportByFindIdAsync(id);
                return Ok(import);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetImportReceiptsByDay")]
        public async Task<IActionResult> GetImportReceiptsByDay(DateTime fromDay,DateTime toDay)
        {
            try
            {
                var import = await _unitOfWork.ImportReceiptsRepository.GetImportFindByDayAsync(fromDay,toDay);
                return Ok(import);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
