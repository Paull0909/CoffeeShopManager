using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class User:IdentityUser<Guid>
    {
        public DateTime CreateDate { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public int EmployeeID { get; set; }
        public List<ExportReceipts> ExportReceipts { get; set; }
        public List<ImportReceipts> ImportReceipts { get; set; }
        public List<Materials> Materials { get; set; }
        public List<Positions> Positions { get; set; }
        public List<Salaries> Salaries { get; set; }
        public Employees Employees { get; set; }
    }
}
