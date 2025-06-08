using Data.DTO.ImportDetails;

namespace Data.DTO.ImportReceipts
{
    public class CreateImportReceipts
    {
        public ImportReceiptsCreateUpdateRequest Receipt { get; set; }
        public List<ImportDetailsCreateUpdateRequest> Details { get; set; }
    }
}
