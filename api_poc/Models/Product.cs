using System.ComponentModel.DataAnnotations.Schema;

namespace api_poc.Models
{
    public class Product
    {
        #region Properties

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        #endregion

        #region Constructors

        public Product(string name, string image, double price, string description)
        {
            this.Name = name;
            this.Image = image;
            this.Price = price;
            this.Description = description;
        }

        #endregion
    }
}
