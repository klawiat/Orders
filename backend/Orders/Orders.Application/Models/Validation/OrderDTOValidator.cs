using FluentValidation;
using Orders.Application.Models.DTOs;

namespace Orders.Application.Models.Validation
{
    public class OrderDTOValidator : AbstractValidator<OrderDTO>
    {
        public OrderDTOValidator()
        {
            RuleFor(dto => dto.Id).NotNull().NotEmpty().NotEqual(Guid.Empty).WithMessage("Неправильный идентификатор заказа");
            RuleForEach(dto => dto.Items).SetValidator(new OrderItemDTOValidator());
        }
    }
}
