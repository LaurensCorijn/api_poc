namespace api_poc.Models.IService
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Add(Product product);
        void SaveChanges();
    }
}
