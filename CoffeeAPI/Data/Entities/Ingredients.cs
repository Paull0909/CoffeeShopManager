using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Ingredients
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Unit { get; set; }
        public List<Recipes> Recipes { get; set; }
    }
}
