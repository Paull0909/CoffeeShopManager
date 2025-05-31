using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data.EF
{
    public class WebDbContextFactory : IDesignTimeDbContextFactory<Web_Context>
    {
        public Web_Context CreateDbContext(string[] args)
        {
            // Đường dẫn tới thư mục chứa appsettings.json
            var appSettingsPath = Path.Combine("D:\\CoffeeShopManager\\CoffeeAPI\\CoffeeAPI");
            // Kiểm tra file appsettings.json có tồn tại không
            if (!File.Exists(Path.Combine(appSettingsPath, "appsettings.json")))
            {
                throw new FileNotFoundException("Không tìm thấy file appsettings.json", Path.Combine(appSettingsPath, "appsettings.json"));
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(appSettingsPath) // Thiết lập đường dẫn đúng
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<Web_Context>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SQLServerIdentityConnection"));

            return new Web_Context(optionsBuilder.Options);
        }
    }
}
