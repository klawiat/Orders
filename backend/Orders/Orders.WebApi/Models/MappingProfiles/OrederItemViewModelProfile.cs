using AutoMapper;
using Orders.Application.Models.DTOs;
using Orders.WebApi.Models.ViewModels;

namespace Orders.WebApi.Models.MappingProfiles
{
    public class OrederItemViewModelProfile : Profile
    {
        public OrederItemViewModelProfile()
        {
            this.CreateMap<OrderItemViewModel, OrderItemDTO>()
                .ForMember(dto => dto.Quantity, opt => opt.MapFrom(vm => vm.Quantity))
                //.ForMember(dto => dto.ProductId, opt => opt.MapFrom(vm => vm.ProductId))
                .AfterMap((vm, dto) => dto.ProductId = Guid.Parse(vm.ProductId));
            this.CreateMap<OrderItemDTO, OrderItemViewModel>()
                //.ForMember(vm => vm.ProductId, opt => opt.MapFrom(dto => dto.ProductId))
                .ForMember(vm => vm.Quantity, opt => opt.MapFrom(dto => dto.Quantity))
                .AfterMap((dto, vm) => vm.ProductId = dto.ProductId.ToString());
        }
    }
}
