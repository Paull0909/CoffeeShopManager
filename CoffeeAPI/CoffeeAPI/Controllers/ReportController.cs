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
                int or = _unitOfWork.OrdersRepository.Find(x => x.OrderDate.Date >= start.Date && x.OrderDate.Date <= end.Date).Count();
                decimal total = _unitOfWork.OrdersRepository.Find(x => x.OrderDate.Date >= start.Date && x.OrderDate.Date <= end.Date).Sum(x => x.FinalAmount);
                var orpr = _unitOfWork.OrdersRepository.Find(x => x.OrderDate.Date >= start.Date && x.OrderDate.Date <= end.Date).ToList();
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

[HttpGet("GetReport_For_Ingredients")]
public async Task<IActionResult> GetReportForIngredients(DateTime start, DateTime end)
{
    try
    {
        // 1. Lấy danh sách đơn hàng trong khoảng thời gian
        var orders = _unitOfWork.OrdersRepository.Find(x => x.OrderDate.Date >= start.Date && x.OrderDate.Date <= end.Date).ToList();

        // 2. Lấy danh sách OrderDetails tương ứng
        var allDetails = new List<OrderDetailsViewModel>();
        foreach (var order in orders)
        {
            var details = _unitOfWork.OrderDetailsRepository.Find(x => x.OrderID == order.OrderID).ToList();
            foreach (var detail in details)
            {
                var mappedDetail = _mapper.Map<OrderDetailsViewModel>(detail);
                allDetails.Add(mappedDetail);
            }
        }

        // 3. Gom nhóm theo ProductID + SizeID
        var productReports = allDetails
            .GroupBy(od => new { od.ProductID, od.SizeID, od.SizeName })
            .Select(g => new ProductDetailReport
            {
                ProductID = g.Key.ProductID,
                SizeID = g.Key.SizeID,
                SizeName = g.Key.SizeName,
                Quantity = g.Sum(od => od.Quantity)
            })
            .ToList();

        // 4. Gán ProductName
        foreach (var item in productReports)
        {
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(item.ProductID);
            item.ProductName = product?.ProductName ?? "Không rõ";
        }

        // 5. Lấy dữ liệu Recipes và Ingredients
        var recipes = await _unitOfWork.RecipesRepository.GetAllAsync();
        var ingredients = await _unitOfWork.IngredientsRepository.GetAllAsync();

        // 6. Tính toán lượng nguyên liệu sử dụng
        var ingredientsReport = recipes
            .Join(productReports,
                  recipe => recipe.ProductSizeID,
                  report => report.SizeID,
                  (recipe, report) => new
                  {
                      recipe.IngredientsID,
                      UsedQuantity = recipe.Quantity * report.Quantity,
                      ProductDetail = new ProductDetailReport
                      {
                          ProductID = report.ProductID,
                          SizeID = report.SizeID,
                          ProductName = report.ProductName,
                          SizeName = report.SizeName,
                          Quantity = report.Quantity
                      }
                  })
            .GroupBy(x => x.IngredientsID)
            .Select(g =>
            {
                var ingredientInfo = ingredients.FirstOrDefault(i => i.Id == g.Key);
                return new IngredientsReport
                {
                    Name = ingredientInfo?.Name ?? "Không rõ",
                    Unit = ingredientInfo?.Unit ?? "",
                    Quantity = g.Sum(x => x.UsedQuantity),
                    productDetails = g.Select(x => x.ProductDetail).ToList()
                };
            })
            .ToList();

        return Ok(ingredientsReport);
    }
    catch (Exception ex)
    {
        return BadRequest(new { error = ex.Message });
    }
}


    }
}
