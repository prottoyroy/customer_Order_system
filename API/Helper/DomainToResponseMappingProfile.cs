using AutoMapper;
using AutoMapperTutorial.Contracts.v1.Responses;
using Core.Models;

namespace API.Helper
{
    public class DomainToResponseMappingProfile:Profile
    {
        public DomainToResponseMappingProfile()
        {
           
            CreateMap<Product, ProductResponse>();
            CreateMap<Order, OrderResponse>();
            CreateMap<OrderItem, OrderItemResponse>();
        }
        
    }
}