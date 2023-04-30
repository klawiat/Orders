using AutoMapper;
using Orders.Application.Models.DTOs;
using Orders.Domain.Entities;

namespace Orders.Application.Models.Mapping
{
    public class OrderDTOProfile : Profile
    {
        public OrderDTOProfile()
        {
            this.CreateMap<OrderDTO, Order>()
                .ForMember(order => order.Lines, opt => opt.MapFrom(dto => dto.Items))
                .AfterMap((dto, order) => order.Lines.ForEach(item => item.OrderId = dto.Id));
            this.CreateMap<Order, OrderDTO>()
                .ForMember(dto => dto.Items, opt => opt.MapFrom(order => order.Lines));
        }
    }
}
