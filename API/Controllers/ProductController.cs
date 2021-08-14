using System.Threading.Tasks;
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

    }
}