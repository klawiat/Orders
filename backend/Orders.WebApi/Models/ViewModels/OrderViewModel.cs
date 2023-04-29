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
        public Guid id { get; set; }
        public string status { get; set; }
        public DateTime? created { get; set; }
        public List<RelationViewModel> lines { get; set; }
    }
}
