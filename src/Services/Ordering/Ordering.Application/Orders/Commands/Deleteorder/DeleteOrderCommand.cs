using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.Deleteorder
{
  
        public record DeleteOrderCommand(Guid orderId) : ICommand<DeleteOrderResult>
        {
        }
        public record DeleteOrderResult(bool IsSuccess);

        public class DeleteOrderCommandValidation : AbstractValidator<DeleteOrderCommand>
        {
            public DeleteOrderCommandValidation()
            {
            RuleFor(x => x.orderId).NotEmpty().WithMessage("Order is Required");
            }
        }
    
}
