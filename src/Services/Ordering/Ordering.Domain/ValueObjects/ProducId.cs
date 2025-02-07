using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record ProducId
    {
        public Guid Value { get; set; }
        private ProducId(Guid value) => Value = value;

        public static ProducId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                throw new DomainException("ProducId can't be empty");
            }

            return new ProducId(value);
        }
    }
}
