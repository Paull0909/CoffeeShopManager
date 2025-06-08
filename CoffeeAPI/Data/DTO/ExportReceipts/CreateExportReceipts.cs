using Data.DTO.ExportDetails;

namespace Data.DTO.ExportReceipts
{
    public class CreateExportReceipts
    {
        public ExportReceiptsCreateUpdateRequest Receipt { get; set; }
        public List<ExportDetailsCreateUpdateRequest> Details { get; set; }
    }
}
