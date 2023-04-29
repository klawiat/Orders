using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Oreders.Domain.Enums;
using Swashbuckle.Swagger.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.WebApi.Models.ViewModels
{
    public class OrderEditViewModel
    {
        public Guid id { get; set; }
        [Required]
        public string status { get; set; }
        [Required]
        public List<RelationViewModel> lines { get; set; }
    }
}
