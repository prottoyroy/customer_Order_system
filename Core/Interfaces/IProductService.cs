using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync(string sortBy ,string searchString,int? pageNumber);
        Task<Product> GetProductAsync(int productId);
        Task<bool> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(int productId, Product product);
       void DeleteProduct(int productId);
        
    }
}