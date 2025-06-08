using Application.SeedWorks;
using AutoMapper;
using Data.DTO.ExportDetails;
using Data.DTO.ExportReceipts;
using Data.DTO.ImportDetails;
using Data.DTO.Lot;
using Data.DTO.LotDetails;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportReceiptsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ExportReceiptsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllExport")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var ex = await _unitOfWork.ExportReceiptsRepository.GetAllAsync();
                return Ok(ex);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExportReceipts request)
        {
            try
            {
                var i = _mapper.Map<ExportReceipts>(request.Receipt);
                _unitOfWork.ExportReceiptsRepository.Add(i);
                var resutl = await _unitOfWork.CompleteAsync();
                if (resutl > 0)
                {
                    foreach (var item in request.Details)
                    {
                        var lot = await _unitOfWork.LotRepository.GetByIdAsync(item.LotID);
                        var id = i.ExportID;
                        item.ExportID = id;

                        var exprot = _mapper.Map<ExportDetailsCreateUpdateRequest, ExportDetails>(item);
                        _unitOfWork.ExportDetailsRepository.Add(exprot);

                        var lotdetails= await _unitOfWork.LotRepository.GetLotDetailsByLotId(lot.LotID);

                        var lotdetailsnew = new LotDetailsCreateUpdateRequets()
                        {
                            LotId = lot.LotID,
                            Quantity = (int)exprot.Quantity,
                            Status = "Tru so luong",
                            QuantityBefor=lotdetails.QuantityAfter,
                            QuantityAfter=lotdetails.QuantityAfter - (int)exprot.Quantity,
                            CreateAt = DateTime.Now,
                        };
                        var lotdetailsresult = _mapper.Map<LotDetailsCreateUpdateRequets, LotDetails>(lotdetailsnew);
                        var materials = await _unitOfWork.MaterialsRepository.GetByIdAsync(lot.MaterialID);
                        materials.TotalMaterial -= (int)item.Quantity;
                        lot.Quantity-=item.Quantity;
                        _unitOfWork.LotDeatailsRepository.Add(lotdetailsresult);

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
        [HttpGet("GetExportDetailsByExportID")]
        public async Task<IActionResult> GetExportDetailsByExportID(int exportID)
        {
            try
            {
                var ip = await _unitOfWork.ExportReceiptsRepository.GetExportDetails(exportID);
                return Ok(ip);
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
                var ex = await _unitOfWork.ExportReceiptsRepository.GetByIdAsync(id);
                _unitOfWork.ExportReceiptsRepository.Remove(ex);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ExportReceiptsCreateUpdateRequest request)
        {
            try
            {
                var ex = await _unitOfWork.ExportReceiptsRepository.GetByIdAsync(request.ExportID);
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

        [HttpGet("GetExportReceiptsByDay")]
        public async Task<IActionResult> GetExportReceiptsByDay(DateTime fromDay, DateTime toDay)
        {
            try
            {
                var from = fromDay.Date;
                var to = toDay.Date.AddDays(1).AddTicks(-1); // đến cuối ngày
                var import = await _unitOfWork.ExportReceiptsRepository.GetExportFindByDayAsync(from, to);
                return Ok(import);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetExportReceiptsByID")]
        public async Task<IActionResult> GetExportReceiptsByID(int id)
        {
            try
            {
                var import = await _unitOfWork.ExportReceiptsRepository.GetByIdAsync(id);
                return Ok(import);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
