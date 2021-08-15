using System.Collections.Generic;
using System.Threading.Tasks;
using API.Contacts.v1.Requests;
using AutoMapper;
using AutoMapperTutorial.Contracts.v1.Requests;
using AutoMapperTutorial.Contracts.v1.Responses;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _mapper = mapper;
            _productService = productService;

        }
        [Authorize(Roles="Administrator")]
        [HttpPost("create-product")]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest createProductRequest)
        {
            var product = new Product
            {
                ProductName = createProductRequest.ProductName
            };
            await _productService.CreateProductAsync(product);
            var productResponse = _mapper.Map<ProductResponse>(product);
            return Created(nameof(Create), productResponse);
        }
        
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetAll( int productId)
        {
            var product = await _productService.GetProductAsync(productId);

            if (product == null) return NotFound(new{Status ="error",Message = "no data found"});

            var productResponse = _mapper.Map<ProductResponse>(product);
            return Ok(productResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string sortBy ,string searchString ,int? pageNumber)
        {
            var products = await _productService.GetProductsAsync(sortBy,searchString ,pageNumber);
            var productsResponse = _mapper.Map<List<ProductResponse>>(products);
            return Ok(productsResponse);
        }
         [Authorize(Roles="Administrator")]
        [HttpPut("update-product-by-id{productId}")]
        public async Task<ActionResult> UpdateProduct (int productId ,[FromBody] Product product )
        {
            var updatedProduct  = await _productService.UpdateProductAsync(productId,product);
            return Ok(updatedProduct);
        }
         [Authorize(Roles="Administrator")]
        [HttpDelete("delete-product{productId}")]
        public ActionResult DeleteProduct(int productId)
        {
            _productService.DeleteProduct(productId);
            return Ok(new{status ="success", message = $"product id : {productId} deleted successfully"});
        }

    }
}