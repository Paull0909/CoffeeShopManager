using Application.SeedWorks;
using Application.Service;
using AutoMapper;
using Data.DTO.OrderDetails;
using Data.DTO.Orders;
using Data.DTO.Tables;
using Data.Entities;
using Data.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllOrderByMonth")]
        public async Task<IActionResult> GetAll(DateTime start)
        {
            try
            {
                var eps = _unitOfWork.OrdersRepository.Find(x=>x.OrderDate.Month == start.Month && x.OrderDate.Year == start.Year).ToList();
                return Ok(eps);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetOrderByDate")]
        public async Task<IActionResult> GetByDate(DateTime start, DateTime end)
        {
            try
            {
                var eps = _unitOfWork.OrdersRepository.Find(x => x.OrderDate >= start && x.OrderDate <= end).ToList();
                return Ok(eps);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrder request)
        {
            try
            {
                if (request == null || request.orderDetails == null) return BadRequest();
                //tao order
                var order = new Orders()
                {
                    EmployeeID = request.EmployeeID,
                    OrderDate = DateTime.Now,
                    Discount = request.Discount,
                    PaymentStatus = (Data.Enum.TransactionStatus)request.PaymentStatus,
                    TableNumberID = request.TableNumberID,
                    CodeOrder=request.CodeOrder,
                };
                if(order.PaymentStatus ==Data.Enum.TransactionStatus.Offline)
                {
                    order.OrderStatus = true;
                    _unitOfWork.OrdersRepository.Add(order);
                    await _unitOfWork.CompleteAsync();

                    decimal totalOrderAmount = 0;

                    foreach (var orderdetail in request.orderDetails)
                    {
                        //tao order details
                        var product = await _unitOfWork.ProductsRepository.GetByIdAsync(orderdetail.ProductID);
                        var productsize = await _unitOfWork.ProductSizesRepository.GetByIdAsync(orderdetail.SizeID);

                        // var ord = _mapper.Map<OrderDetails>(j);
                        var ord = new OrderDetails()
                        {
                            OrderID = order.OrderID,
                            ProductID = product.ProductID,
                            SizeID = productsize.ProductSizeID,
                            UnitPrice = product.Price,
                            Quantity = orderdetail.Quantity,
                            TotalPrice = (product.Price + productsize.AdditionalPrice) * orderdetail.Quantity,
                        };
                        _unitOfWork.OrderDetailsRepository.Add(ord);
                        await _unitOfWork.CompleteAsync();

                        decimal totalDetailPrice = ord.TotalPrice;

                        if (orderdetail.OrderToppingDetails != null)
                        {
                            foreach (var toppingDetail in orderdetail.OrderToppingDetails)
                            {
                                var topping = await _unitOfWork.ToppingsRepository.GetByIdAsync(toppingDetail.ToppingID);
                                if (topping == null)
                                    return BadRequest("Topping không tồn tại.");

                                var ordertopping = new OrderToppingDetails()
                                {
                                    OrderDetailID = ord.OrderDetailID,
                                    UnitPrice = topping.Price,
                                    TotalPrice = toppingDetail.Quantity * topping.Price,
                                    Quantity = toppingDetail.Quantity,
                                    ToppingID = topping.ToppingID,

                                };

                                totalDetailPrice += ordertopping.TotalPrice;
                                _unitOfWork.OrderToppingDetailsRepository.Add(ordertopping);
                            }
                            await _unitOfWork.CompleteAsync();

                        }


                        totalOrderAmount += totalDetailPrice;

                    }

                    order.TotalAmount = totalOrderAmount;
                    order.FinalAmount = totalOrderAmount - (totalOrderAmount * order.Discount / 100);
                    await _unitOfWork.CompleteAsync();
                    return Ok();
                }
                else
                {
                    order.OrderStatus = false;
                    _unitOfWork.OrdersRepository.Add(order);
                    await _unitOfWork.CompleteAsync();

                    decimal totalOrderAmount = 0;

                    foreach (var orderdetail in request.orderDetails)
                    {
                        //tao order details
                        var product = await _unitOfWork.ProductsRepository.GetByIdAsync(orderdetail.ProductID);
                        var productsize = await _unitOfWork.ProductSizesRepository.GetByIdAsync(orderdetail.SizeID);

                        // var ord = _mapper.Map<OrderDetails>(j);
                        var ord = new OrderDetails()
                        {
                            OrderID = order.OrderID,
                            ProductID = product.ProductID,
                            SizeID = productsize.ProductSizeID,
                            UnitPrice = productsize.AdditionalPrice,
                            Quantity = orderdetail.Quantity,
                            TotalPrice = (product.Price + productsize.AdditionalPrice) * orderdetail.Quantity,
                        };
                        _unitOfWork.OrderDetailsRepository.Add(ord);
                        await _unitOfWork.CompleteAsync();

                        decimal totalDetailPrice = ord.TotalPrice;

                        if (orderdetail.OrderToppingDetails != null)
                        {
                            foreach (var toppingDetail in orderdetail.OrderToppingDetails)
                            {
                                var topping = await _unitOfWork.ToppingsRepository.GetByIdAsync(toppingDetail.ToppingID);
                                if (topping == null)
                                    return BadRequest("Topping không tồn tại.");
                                var ordertopping = new OrderToppingDetails()
                                {
                                    OrderDetailID = ord.OrderDetailID,
                                    UnitPrice = topping.Price,
                                    TotalPrice = toppingDetail.Quantity * topping.Price,
                                    Quantity = toppingDetail.Quantity,
                                    ToppingID = topping.ToppingID,
                                };

                                totalDetailPrice += ordertopping.TotalPrice;
                                _unitOfWork.OrderToppingDetailsRepository.Add(ordertopping);
                            }
                            await _unitOfWork.CompleteAsync();
                        }
                        totalOrderAmount += totalDetailPrice;
                    }
                    order.TotalAmount = totalOrderAmount;
                    order.FinalAmount = totalOrderAmount - (totalOrderAmount * order.Discount / 100);
                    await _unitOfWork.CompleteAsync();
                    return Ok();
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
                var or = await _unitOfWork.OrdersRepository.GetByIdAsync(id);
                var list = await _unitOfWork.OrderDetailsRepository.GetByOrderID(id);
                foreach (var item in list)
                {
                    _unitOfWork.OrderDetailsRepository.Remove(item);
                }
                _unitOfWork.OrdersRepository.Remove(or);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(OrdersCreateUpdateRequest request)
        {
            try
            {
                var cate = await _unitOfWork.OrdersRepository.GetByIdAsync(request.OrderID);
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

        [HttpPost("generate-code")]
        public async Task<IActionResult> GenerateCode()
        {
            var code = StringGenerator.GenerateRandomString();
            var check = await _unitOfWork.OrdersRepository.CheckCodeOrder(code);
            if(check == true)
            {
                code = StringGenerator.GenerateRandomString();
               
            }
            return Ok(new { Code = code });
        }

        [HttpGet("GetOrderByCodeOrder")]
        public async Task<IActionResult> GetOrderByCodeOrder(string code)
        {
            try
            {
                var eps = await _unitOfWork.OrdersRepository.GetOrderByCodeOrder(code);
                return Ok(eps);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("UpdateOrderByOrderStatus")]
        public async Task<IActionResult> UpdateOrderByOrderStatus(int id,bool status)
        {
            try
            {
                var eps = await _unitOfWork.OrdersRepository.UpdateOrderByOrderStatus(id, status);
                return Ok(eps);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("BankTransferToCash")]
        public async Task<IActionResult> BankTransferToCash(int id,TransactionStatus transactionStatus, bool status)
        {
            try
            {
                var eps = await _unitOfWork.OrdersRepository.BankTransferToCash(id,transactionStatus,status);
                return Ok(eps);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
