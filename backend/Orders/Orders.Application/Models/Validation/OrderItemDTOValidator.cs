using FluentValidation;
using Orders.Application.Models.DTOs;

namespace Orders.Application.Models.Validation
{
    public class OrderItemDTOValidator : AbstractValidator<OrderItemDTO>
    {
        public OrderItemDTOValidator()
        {
            RuleFor(dto => dto.ProductId).NotNull().NotEmpty().NotEqual(Guid.Empty).WithMessage("Неправильный идентификатор товара");
            RuleFor(dto => dto.Quantity).NotNull().NotEmpty().GreaterThan(0).WithMessage("Нельзя добавить менее 1 товара");
        }
    }
}
