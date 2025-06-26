using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Surcharges
    {
        public int ID { get; set; }
        [Required]
        public string SurchargesName { get; set; }
        public int SurchargesValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
