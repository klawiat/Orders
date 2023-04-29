using AutoMapper;
using Orders.Application.Models.DTOs;
using Orders.WebApi.Models.ViewModels;
using Oreders.Domain.Entity;

namespace Orders.WebApi.Models
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            this.CreateMap<RelationDTO, RelationViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x=>x.ProductId))
                .ForMember(x => x.uty, opt => opt.MapFrom(x=>x.Count));
            this.CreateMap<RelationViewModel, RelationDTO>()
                .ForMember(x => x.ProductId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Count, opt => opt.MapFrom(x => x.uty));
            this.CreateMap<ProductViewModel, ProductDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name));
            this.CreateMap<ProductDTO, ProductViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(o=>o.Id))
                .ForMember(x=>x.Name,opt=>opt.MapFrom(o=>o.Name));
            this.CreateMap<OrderCreateViewModel, OrderDTO>()
                .ForMember(x => x.Relations, opt => opt.MapFrom(x => x.lines))
                .ForMember(x=>x.Id,opt=>opt.MapFrom(o=>o.id));
            this.CreateMap<OrderDTO, OrderCreateViewModel>()
                .ForMember(x => x.lines, opt => opt.MapFrom(x => x.Relations))
                .ForMember(x=>x.id,opt=>opt.MapFrom(o=>o.Id));
            this.CreateMap<OrderEditViewModel, OrderDTO>()
                .ForMember(x => x.Relations, opt => opt.MapFrom(x => x.lines))
                .ForMember(x=>x.Status,opt=>opt.MapFrom(o=>o.status))
                .ForMember(x=>x.Id,opt=>opt.MapFrom(o=>o.id));
            this.CreateMap<OrderDTO, OrderEditViewModel>()
                .ForMember(x => x.lines, opt => opt.MapFrom(x => x.Relations))
                .ForMember(x => x.id, opt => opt.MapFrom(o => o.Id))
                .ForMember(x=>x.status,opt=>opt.MapFrom(o=>o.Status));
            this.CreateMap<OrderDTO, OrderViewModel>()
                .ForMember(x => x.lines, opt => opt.MapFrom(x => x.Relations))
                .ForMember(x => x.id, opt => opt.MapFrom(o => o.Id))
                .ForMember(x => x.created,opt=>opt.MapFrom(o=>o.Created))
                .ForMember(x=>x.status,opt=>opt.MapFrom(o=>o.Status));
            this.CreateMap<OrderViewModel, OrderDTO>()
                .ForMember(x => x.Relations, opt => opt.MapFrom(x => x.lines))
                .ForMember(x => x.Id, opt => opt.MapFrom(o => o.id))
                .ForMember(x => x.Created, opt => opt.MapFrom(o => o.created))
                .ForMember(x => x.Status, opt => opt.MapFrom(o => o.status));
        }
    }
}
