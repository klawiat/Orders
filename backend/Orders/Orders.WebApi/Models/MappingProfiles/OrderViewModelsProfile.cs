using AutoMapper;
using Orders.Application.Models.DTOs;
using Orders.Domain.Enums;
using Orders.WebApi.Models.ViewModels;

namespace Orders.WebApi.Models.MappingProfiles
{
    public class OrderViewModelsProfile : Profile
    {
        public OrderViewModelsProfile()
        {
            this.CreateMap<OrderViewModel, OrderDTO>()
                .ForMember(dto => dto.Items, opt => opt.MapFrom(vm => vm.Items));
            this.CreateMap<OrderDTO, OrderViewModel>()
                .ForMember(vm => vm.Items, opt => opt.MapFrom(dto => dto.Items));
            this.CreateMap<CreateOrderViewModel, OrderDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(vm => vm.Id))
                .ForMember(dto => dto.Items, opt => opt.MapFrom(vm => vm.Items));
            this.CreateMap<EditOrderViewModel, OrderDTO>()
                .ForMember(dto => dto.Items, opt => opt.MapFrom(vm => vm.Items))
                .AfterMap((vm, dto) =>
                {
                    dto.Status = (Status)Enum.Parse(typeof(Status), vm.Status);
                });
        }
    }
}
