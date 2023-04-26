using Microsoft.AspNetCore.Mvc;
using Oreders.Domain.Entity;
using Oreders.Domain.Enums;
using Swashbuckle.Swagger.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Orders.WebApi.Models.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public DateTime? Created { get; set; }
        public List<RelationViewModel> Lines { get; set; }
    }
}
