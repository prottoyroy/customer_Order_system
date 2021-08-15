using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

        public void DeleteProduct(int productId)
        {
            var _product = _context.Products.FirstOrDefault(x =>x.ProductId==productId);
            if(_product == null)
           
            if(_product !=null)
            {
                _context.Remove(_product);
                _context.SaveChanges();
            }
            else{
                throw new Exception($"The product with Id : {productId} does not exist");
            }
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return await _context.Products.SingleOrDefaultAsync(s => s.ProductId == productId);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> UpdateProductAsync(int productId, Product product)
        {
            var _product = await _context.Products.FirstOrDefaultAsync(x =>x.ProductId ==productId) ;
            if(_product !=null)
            {
                _product.ProductName = product.ProductName;
                _product.Price =product.Price;
                _context.SaveChanges();

            }
            return _product;
        }

    }
}