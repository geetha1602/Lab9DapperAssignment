using Lab9CrudUsingDapper.Models;

namespace Lab9CrudUsingDapper.Services
{
    public interface IProductServices
    {
        public List<Product> GetAllProducts();
        public string InsertProduct(Product product);
        public string UpdateProduct(Product product);
        public string DeleteProduct(int ProduuctId);

    }
}
