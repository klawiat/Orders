using Oreders.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Oreders.Domain.Extensions
{
    public static class EnumExtension
    {
        public static string GetDisplayName(this System.Enum enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                ?.GetName() ?? "Неопределенный";
        }
        public static Status ToEnum(this string status)
        {
            Status converted;
            if (Enum.TryParse(status, true, out converted))
                return converted;
            else
                return Status.New;
        }
    }
}
