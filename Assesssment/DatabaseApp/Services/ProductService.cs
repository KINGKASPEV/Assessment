using DatabaseApp.Context;
using DatabaseApp.Models;

namespace DatabaseApp.Services
{
    public class ProductService
    {
        private readonly ProductDbContext _dbContext;

        public ProductService()
        {
            _dbContext = new ProductDbContext();
        }

        public void AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public List<Product> GetAllProducts()
        {
            return _dbContext.Products.ToList();
        }
    }

}
