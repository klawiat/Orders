using AutoMapper;
using Orders.WebApi.Models.ViewModels;
using Oreders.Domain.Entity;

namespace Orders.WebApi.Models
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Product, ProductViewModel>();
            this.CreateMap<ProductViewModel, Product>();
            this.CreateMap<Relation, RelationViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x=>x.ProductId))
                .ForMember(x => x.uty, opt => opt.MapFrom(x=>x.Count));
            this.CreateMap<RelationViewModel, Relation>()
                .ForMember(x => x.ProductId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Count, opt => opt.MapFrom(x => x.uty));
            this.CreateMap<OrderCreateViewModel, Order>()
                .ForMember(x => x.Relations, opt => opt.MapFrom(x => x.Lines));
            this.CreateMap<Order, OrderCreateViewModel>()
                .ForMember(x => x.Lines, opt => opt.MapFrom(x => x.Relations));
            this.CreateMap<OrderEditViewModel, Order>()
                .ForMember(x => x.Relations, opt => opt.MapFrom(x => x.Lines));
            this.CreateMap<Order, OrderEditViewModel>()
                .ForMember(x => x.Lines, opt => opt.MapFrom(x => x.Relations));
            this.CreateMap<Order, OrderViewModel>()
                .ForMember(x => x.Lines, opt => opt.MapFrom(x => x.Relations));
            this.CreateMap<OrderViewModel, Order>()
                .ForMember(x => x.Relations, opt => opt.MapFrom(x => x.Lines));
        }
    }
}
