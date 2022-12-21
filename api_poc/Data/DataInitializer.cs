namespace api_poc.Data
{
    public class DataInitializer
    {
        private readonly DataContext _context;
        
        public DataInitializer(DataContext dbContext)
        {
            _context = dbContext;
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
