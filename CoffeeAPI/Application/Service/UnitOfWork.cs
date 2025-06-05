using Application.IRepositoty;
using Application.Repositoty;
using Application.SeedWorks;
using AutoMapper;
using Data.Context;

namespace Application.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Web_Context _context;

        public UnitOfWork(Web_Context context, IMapper mapper)
        {
            _context = context;
            Categories_MaterialRepository = new Categories_MaterialRepository(context, mapper);
            Categories_ProductsRepository = new Categories_ProductsRepository(context, mapper);
            EmployeesRepository = new EmployeesRepository(context, mapper);
            EmployeeSchedulesRepository = new EmployeeSchedulesRepository(context, mapper);
            ExportDetailsRepository = new ExportDetailsRepository(context, mapper);
            ExportReceiptsRepository = new ExportReceiptsRepository(context, mapper);
            ImportDetailsRepository = new ImportDetailsRepository(context, mapper);
            ImportReceiptsRepository = new ImportReceiptsRepository(context, mapper);
            InventoryLogsRepository = new InventoryLogsRepository(context, mapper);
            MaterialsRepository = new MaterialsRepository(context, mapper);
            LotRepository = new LotRepository(context, mapper);
            OrderDetailsRepository = new OrderDetailsRepository(context, mapper);
            UserRepository = new UserRepository(context, mapper);
            OrdersRepository = new OrdersRepository(context, mapper);
            PaymentsRepository = new PaymentsRepository(context, mapper);
            PositionsRepository = new PositionsRepository(context, mapper);
            ProductsRepository = new ProductsRepository(context, mapper);
            ProductSizesRepository = new ProductSizesRepository(context, mapper);
            RecipesRepository = new RecipesRepository(context, mapper);
            SalariesRepository = new SalariesRepository(context, mapper);
            ShiftsRepository = new ShiftsRepository(context, mapper);
            SuppliersRepository = new SuppliersRepository(context, mapper);
            TablesRepository = new TablesRepository(context, mapper);
            TimekeepingRepository = new TimekeepingRepository(context, mapper);
            ToppingsRepository = new ToppingsRepository(context, mapper);
        }
        public ICategories_MaterialRepository Categories_MaterialRepository { get; private set; }
        public ICategories_ProductsRepository Categories_ProductsRepository { get; private set; }
        public IEmployeesRepository EmployeesRepository { get; private set; }
        public IEmployeeSchedulesRepository EmployeeSchedulesRepository { get; private set; }
        public IExportDetailsRepository ExportDetailsRepository { get; private set; }
        public IExportReceiptsRepository ExportReceiptsRepository { get; private set; }
        public IImportDetailsRepository ImportDetailsRepository { get; private set; }
        public IImportReceiptsRepository ImportReceiptsRepository { get; private set; }
        public IInventoryLogsRepository InventoryLogsRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }
        public IMaterialsRepository MaterialsRepository { get; private set; }
        public IOrderDetailsRepository OrderDetailsRepository { get; private set; }
        public IOrdersRepository OrdersRepository { get; private set; }
        public IPaymentsRepository PaymentsRepository { get; private set; }
        public IPositionsRepository PositionsRepository { get; private set; }
        public IProductsRepository ProductsRepository { get; private set; }
        public IProductSizesRepository ProductSizesRepository { get; private set; }
        public IRecipesRepository RecipesRepository { get; private set; }
        public ISalariesRepository SalariesRepository { get; private set; }
        public IShiftsRepository ShiftsRepository { get; private set; }
        public ISuppliersRepository SuppliersRepository { get; private set; }
        public ITablesRepository TablesRepository { get; private set; }
        public ITimekeepingRepository TimekeepingRepository { get; private set; }
        public IToppingsRepository  ToppingsRepository { get; private set; }

        public ILotRepository LotRepository { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}