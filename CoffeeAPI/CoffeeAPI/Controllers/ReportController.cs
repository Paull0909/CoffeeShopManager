using Application.SeedWorks;
using AutoMapper;
using Data.DTO.OrderDetails;
using Data.DTO.Products;
using Data.DTO.Report;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ReportController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetReport_For_Month")]
        public async Task<IActionResult> GetByToMontDay()
        {
            try
            {
                var time = DateTime.Now;
                var report = new ReportViewModel();
                var list = new  List<OrderDetailsViewModel>();
                int ep = _unitOfWork.EmployeesRepository.Find(x => x.Status == true).Count();
                int or = _unitOfWork.OrdersRepository.Find(x => x.OrderDate.Month == time.Month && x.OrderDate.Year == time.Year).Count();
                decimal total = _unitOfWork.OrdersRepository.Find(x => x.OrderDate.Month == time.Month && x.OrderDate.Year == time.Year).Sum(x => x.FinalAmount);
                var orpr = _unitOfWork.OrdersRepository.Find(x => x.OrderDate.Month == time.Month && x.OrderDate.Year == time.Year).ToList();
                foreach(var i in orpr)
                {
                    var j = _unitOfWork.OrderDetailsRepository.Find(x => x.OrderID == i.OrderID).ToList();
                    foreach(var item in j)
                    {
                        var k = _mapper.Map<OrderDetailsViewModel>(item);
                        list.Add(k);
                    }
                }
                var result = list.GroupBy(od => od.ProductID)
                            .Select(g => new ProductReport()
                            {
                                ProductID = g.Key,
                                Quantity = g.Sum(od => od.Quantity)
                            })
                            .ToList();
                foreach(var item in result)
                {
                    var i = await _unitOfWork.ProductsRepository.GetByIdAsync(item.ProductID);
                    item.ProductName = i.ProductName;
                }
                report.Products = result;
                report.Number_Orders = or;
                report.Number_Employee = ep;
                report.TotalRevenue = total;
                return Ok(report);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetReport_For_ToDay")]
        public async Task<IActionResult> GetByToDay()
        {
            try
            {
                var time = DateTime.Now;
                var report = new ReportViewModel();
                var list = new List<OrderDetailsViewModel>();
                int ep = _unitOfWork.EmployeesRepository.Find(x => x.Status == true).Count();
                int or = _unitOfWork.OrdersRepository.Find(x => x.OrderDate.Month == time.Month && x.OrderDate.Year == time.Year).Count();
                decimal total = _unitOfWork.OrdersRepository.Find(x =>x.OrderDate.Date == time.Date).Sum(x => x.FinalAmount);
                var orpr = _unitOfWork.OrdersRepository.Find(x => x.OrderDate.Month == time.Month && x.OrderDate.Year == time.Year).ToList();
                foreach (var i in orpr)
                {
                    var j = _unitOfWork.OrderDetailsRepository.Find(x => x.OrderID == i.OrderID).ToList();
                    foreach (var item in j)
                    {
                        var k = _mapper.Map<OrderDetailsViewModel>(item);
                        list.Add(k);
                    }
                }
                var result = list.GroupBy(od => od.ProductID)
                            .Select(g => new ProductReport()
                            {
                                ProductID = g.Key,
                                Quantity = g.Sum(od => od.Quantity)
                            })
                            .ToList();
                foreach (var item in result)
                {
                    var i = await _unitOfWork.ProductsRepository.GetByIdAsync(item.ProductID);
                    item.ProductName = i.ProductName;
                }
                report.Products = result;
                report.Number_Orders = or;
                report.Number_Employee = ep;
                report.TotalRevenue = total;
                return Ok(report);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetReport_For_Date")]
        public async Task<IActionResult> GetByDate(DateTime start, DateTime end)
        {
            try
            {
                var report = new ReportViewModel();
                var list = new List<OrderDetailsViewModel>();
                int ep = _unitOfWork.EmployeesRepository.Find(x => x.Status == true).Count();
                int or = _unitOfWork.OrdersRepository.Find(x => x.OrderDate >= start && x.OrderDate <= end).Count();
                decimal total = _unitOfWork.OrdersRepository.Find(x => x.OrderDate >= start && x.OrderDate <= end).Sum(x => x.FinalAmount);
                var orpr = _unitOfWork.OrdersRepository.Find(x => x.OrderDate >= start && x.OrderDate <= end).ToList();
                foreach (var i in orpr)
                {
                    var j = _unitOfWork.OrderDetailsRepository.Find(x => x.OrderID == i.OrderID).ToList();
                    foreach (var item in j)
                    {
                        var k = _mapper.Map<OrderDetailsViewModel>(item);
                        list.Add(k);
                    }
                }
                var result = list.GroupBy(od => od.ProductID)
                            .Select(g => new ProductReport()
                            {
                                ProductID = g.Key,
                                Quantity = g.Sum(od => od.Quantity)
                            })
                            .ToList();
                foreach (var item in result)
                {
                    var i = await _unitOfWork.ProductsRepository.GetByIdAsync(item.ProductID);
                    item.ProductName = i.ProductName;
                }
                report.Products = result;
                report.Number_Orders = or;
                report.Number_Employee = ep;
                report.TotalRevenue = total;
                return Ok(report);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
