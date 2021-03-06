using System.Collections.Generic;
using System.Linq;
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
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _mapper = mapper;
            _orderService = orderService;

        }
        [HttpPost("create-orders")]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest createOrderRequest)
        {
            var order = new Order
            {
                ApplicationUserId = createOrderRequest.CustomerId,
                OrderDate = createOrderRequest.OrderDate,
                OrderItems = createOrderRequest.OrderItems.ToList().Select(s => new OrderItem
                {
                    ProductId = s.ProductId,
                    Quantity = s.Quantity
                }).ToList()
            };
            await _orderService.CreateOrderAsync(order);
            var orderResponse = _mapper.Map<OrderResponse>(order);
            return Created(nameof(Create), order);
        }
        [Authorize(Roles="Administrator")]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderAsync([FromRoute] int orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);
            var orderResponse = _mapper.Map<OrderResponse>(order);
            return Ok(orderResponse);
        }
       // [Authorize(Roles="Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var order = await _orderService.GetOrdersAsync();
            var orderResponse = _mapper.Map<List<OrderResponse>>(order);
            return Ok(orderResponse);
        }
    }
}