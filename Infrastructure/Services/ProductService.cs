using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<bool> CreateProductAsync(Product product)
        {
            var data = await _context.Products.AddAsync(product);
            var createdRowCount = await _context.SaveChangesAsync();
            return createdRowCount>0;

        }

        public Task<Product> GetProductAsync(int productId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Product>> GetProductsAsync()
        {
            throw new System.NotImplementedException();
        }

        
    }
}