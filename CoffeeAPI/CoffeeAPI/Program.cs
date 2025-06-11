using Application.SeedWorks;
using Application.Service;
using AutoMapper;
using Data.Context;
using Data.DTO.Categories_Material;
using Data.DTO.Categories_Products;
using Data.DTO.Employees;
using Data.DTO.EmployeeSchedules;
using Data.DTO.ExportDetails;
using Data.DTO.ExportReceipts;
using Data.DTO.ImportDetails;
using Data.DTO.ImportReceipts;
using Data.DTO.InventoryLogs;
using Data.DTO.Lot;
using Data.DTO.LotDetails;
using Data.DTO.Materials;
using Data.DTO.OrderDetails;
using Data.DTO.Orders;
using Data.DTO.OrderToppingDetails;
using Data.DTO.Payments;
using Data.DTO.Positions;
using Data.DTO.Products;
using Data.DTO.ProductSizes;
using Data.DTO.Recipes;
using Data.DTO.Salaries;
using Data.DTO.Shifts;
using Data.DTO.Suppliers;
using Data.DTO.Tables;
using Data.DTO.Timekeeping;
using Data.DTO.Toppings;
using Data.DTO.User;
using Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SQLServerIdentityConnection") ?? throw new InvalidOperationException("Connection string 'SQLServerIdentityConnection' not found.");
builder.Services.AddDbContext<Web_Context>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Data"))); // Chỉ định assembly chứa migrations

builder.Services.AddControllers();

//Register Mapper ModelDTO
builder.Services.AddAutoMapper(typeof(Categories_MaterialCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(Categories_ProductsCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(EmployeesCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(EmployeeSchedulesCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(ExportDetailsCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(ExportReceiptsCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(ImportDetailsCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(ImportReceiptsCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(InventoryLogsCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(MaterialsCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(OrderDetailsCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(OrdersCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(PaymentsCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(PositionsCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(ProductsCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(ProductSizesCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(RecipesCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(SalariesCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(ShiftsCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(SuppliersCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(TablesCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(TimekeepingCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(ToppingsCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(LotCreateUpdateRequest));
builder.Services.AddAutoMapper(typeof(LotDetailsCreateUpdateRequets));
builder.Services.AddAutoMapper(typeof(OrderToppingDetailsCreateUpdateReuqest));
//Authen and author
builder.Services.Configure<JwtTokenSettings>(configuration.GetSection("JwtTokenSettings"));
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<Web_Context>()
    .AddDefaultTokenProviders();
//Register Repositories 
builder.Services.AddScoped(typeof(IRepository<,>), typeof(RepositoryBase<,>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<SignInManager<User>, SignInManager<User>>();
builder.Services.AddScoped<UserManager<User>, UserManager<User>>();
builder.Services.AddScoped<RoleManager<Role>, RoleManager<Role>>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomOperationIds(apiDesc =>
    {
        return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
    });
    c.SwaggerDoc("Admin", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "API for Administrators",
        Description = "API for CMS core domain. This domain keeps track of campaigns, campaign rules, and campaign execution."
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Input JWT token at input bottom. Example: Bearer {your token}"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    c.ParameterFilter<SwaggerNullableParameterFilter>();
});
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = configuration["JwtTokenSettings:Issuer"],
        ValidAudience = configuration["JwtTokenSettings:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtTokenSettings:Key"]))
    };
});

// Configure logging provider
builder.Logging.ClearProviders(); // Optional: Clears default loggingproviders
builder.Logging.AddConsole(); // Adds console logging provider
builder.Logging.AddDebug(); // Adds Debug logging provider
                            // Set specific log levels for different librarues
builder.Logging.SetMinimumLevel(LogLevel.Information); // Default minimum level
builder.Logging.AddFilter("Microsoft", LogLevel.Warning); // For Microsoft.* namespaces
builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning); // For ASP.NET Core
builder.Logging.AddFilter("System", LogLevel.Warning); // For System.* namespaces
builder.Logging.AddFilter("YourApp.Namespace", LogLevel.Trace); // For your custom namespace




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("Admin/swagger.json", "Admin");
        c.DisplayOperationId();
        c.DisplayRequestDuration();
    });
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
