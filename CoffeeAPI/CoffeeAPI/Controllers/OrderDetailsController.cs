using Application.SeedWorks;
using AutoMapper;
using Data.DTO.OrderDetails;
using Data.DTO.OrderToppingDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public OrderDetailsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("GetAllOrderDetailByBillID")]
        public async Task<IActionResult> GetAll(int id)
        {
            try
            {
                var od = _unitOfWork.OrderDetailsRepository.Find(x=>x.OrderID == id).ToList();
                var result = new List<OrderDetailsViewModel>();
                foreach(var item in od)
                {
                    var i = _mapper.Map<OrderDetailsViewModel>(item);
                    var size = await _unitOfWork.ProductSizesRepository.GetByIdAsync(i.SizeID);
                    i.SizeName = size.SizeName;
                    var j = _unitOfWork.OrderToppingDetailsRepository.Find(x => x.OrderDetailID == i.OrderDetailID).ToList();
                    var list = new List<OrderToppingDetailsViewModel>();
                    foreach(var t in j)
                    {
                        var k = _mapper.Map<OrderToppingDetailsViewModel>(t);
                        list.Add(k);
                    }
                    i.listtopping = list;
                    result.Add(i);
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
