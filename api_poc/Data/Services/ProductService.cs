using api_poc.Models;
using api_poc.Models.IService;
using Microsoft.EntityFrameworkCore;

namespace api_poc.Data.Services
{
    public class ProductService: IProductService
    {
        private readonly DataContext _dbContext;
        private readonly DbSet<Product> _products;

        public ProductService(DataContext dbContext)
        {
            _dbContext = dbContext;
            _products = _dbContext.Products;
        }

        public IEnumerable<Product> GetAll()
        {
            return _products.ToList();
        }

        public Product GetById(int id)
        {
            return _products.SingleOrDefault(p => p.Id == id);
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

    }
}
