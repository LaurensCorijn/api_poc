using System.ComponentModel.DataAnnotations;

namespace api_poc.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
