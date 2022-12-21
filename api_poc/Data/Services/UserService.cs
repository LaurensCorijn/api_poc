using api_poc.Models;
using api_poc.Models.IService;
using Microsoft.EntityFrameworkCore;

namespace api_poc.Data.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dbContext;
        private readonly DbSet<User> _users;

        public UserService(DataContext dbContext)
        {
            _dbContext = dbContext;
            _users = _dbContext.Users;
        }

        public void Add(User user)
        {
            _users.Add(user);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}