using AutoMapper;
using Orders.Application.Models.DTOs;
using Orders.Domain.Entities;

namespace Orders.Application.Models.Mapping
{
    public class OrderItemDTOProfile : Profile
    {
        public OrderItemDTOProfile()
        {
            this.CreateMap<OrderItemDTO, OrderItem>();
            this.CreateMap<OrderItem, OrderItemDTO>();
        }
    }
}
