using Microsoft.AspNetCore.Identity;
using api_poc.Models;

namespace api_poc.Data
{
    public class DataInitializer
    {
        private readonly DataContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        
        public DataInitializer(DataContext dbContext, UserManager<IdentityUser> userManager)
        {
            _context = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _context.Database.EnsureDeleted();
            if (_context.Database.EnsureCreated())
            {
                var products = new List<Product>
                {
                    new Product("testName", "testImage", 29.0, "testDescription")
                };

                _context.Products.AddRange(products);

                _context.SaveChanges();
            }
        }
    }
}
