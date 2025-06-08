using Application.IRepositoty;

namespace Application.SeedWorks
{
    public interface IUnitOfWork
    {
        ICategories_MaterialRepository Categories_MaterialRepository { get; }
        ICategories_ProductsRepository Categories_ProductsRepository { get; }
        IEmployeeSchedulesRepository EmployeeSchedulesRepository { get; }
        IEmployeesRepository EmployeesRepository { get; }
        IExportDetailsRepository ExportDetailsRepository { get; }
        IExportReceiptsRepository ExportReceiptsRepository { get; }
        IImportDetailsRepository ImportDetailsRepository { get; }
        IImportReceiptsRepository ImportReceiptsRepository { get; }
        IInventoryLogsRepository InventoryLogsRepository { get; }
        IMaterialsRepository MaterialsRepository { get; }
        ILotRepository LotRepository { get; }
        IOrderDetailsRepository OrderDetailsRepository { get; }
        IOrdersRepository OrdersRepository { get; }
        IPaymentsRepository PaymentsRepository { get; }
        IPositionsRepository PositionsRepository { get; }
        IProductSizesRepository ProductSizesRepository { get; }
        IProductsRepository ProductsRepository { get; }
        IRecipesRepository RecipesRepository { get; }
        ISalariesRepository SalariesRepository { get; }
        IShiftsRepository ShiftsRepository { get; }
        ISuppliersRepository SuppliersRepository { get; }
        ITablesRepository TablesRepository { get; }
        ITimekeepingRepository TimekeepingRepository { get; }
        IToppingsRepository ToppingsRepository { get; }
        ILotDeatailsRepository LotDeatailsRepository { get; }  
        Task<int> CompleteAsync();
    }
}
