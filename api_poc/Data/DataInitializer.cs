using Microsoft.AspNetCore.Identity;

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

            }
        }
    }
}
