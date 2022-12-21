namespace api_poc.Models.IService
{
    public interface IUserService
    {
        void Add(User user);
        void SaveChanges();
    }
}
