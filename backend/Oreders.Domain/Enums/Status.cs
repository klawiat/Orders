using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oreders.Domain.Enums
{
    public enum Status:int
    {
        [Display(Name = "Новый")]
        New =1,
        [Display(Name = "Ожидает оплату")]
        AwaitingPayment,
        [Display(Name = "Оплачен")]
        Paid,
        [Display(Name ="Передан в доставку")]
        SubmittedForDelivery,
        [Display(Name = "Доставлен")]
        Delivered,
        [Display(Name = "Завершён")]
        Completed
    }
}
