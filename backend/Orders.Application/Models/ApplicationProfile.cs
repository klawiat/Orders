using AutoMapper;
using Orders.Application.Models.DTOs;
using Oreders.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oreders.Domain.Extensions;

namespace Orders.Application.Models
{
    public class ApplicationProfile:Profile
    {
        public ApplicationProfile()
        {
            this.CreateMap<RelationDTO, Relation>()
                .ForMember(x => x.ProductId,opt=>opt.MapFrom(o=>o.ProductId))
                .ForMember(x => x.Count, opt => opt.MapFrom(o => o.Count))
                .ForMember(x => x.OrderId, opt => opt.Ignore());
            this.CreateMap<Relation, RelationDTO>()
                .ForMember(x => x.ProductId, opt => opt.MapFrom(o => o.ProductId))
                .ForMember(x => x.Count, opt => opt.MapFrom(o => o.Count));
            this.CreateMap<ProductDTO, Product>()
                .ForMember(x => x.Id,opt=>opt.MapFrom(o=>o.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(x => x.Relations, opt => opt.Ignore());
            this.CreateMap<Product, ProductDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name));
            this.CreateMap<OrderDTO, Order>()
                .ForMember(x => x.Id,opt=>opt.MapFrom(o=>o.Id))
                .ForMember(x => x.Relations, opt => opt.MapFrom(o => o.Relations))
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .AfterMap((dto, obj) => { obj.Status = dto.Status.ToEnum(); });
            this.CreateMap<Order, OrderDTO>()
                .ForMember(x => x.Id,opt=>opt.MapFrom(o=>o.Id))
                .ForMember(x => x.Created, opt => opt.MapFrom(o => o.Created))
                .ForMember(x => x.Relations, opt => opt.MapFrom(o => o.Relations))
                .ForMember(x => x.Status, opt => opt.Ignore())
                .AfterMap((obj,dto)=>dto.Status=obj.Status.ToString());
        }
    }
}
