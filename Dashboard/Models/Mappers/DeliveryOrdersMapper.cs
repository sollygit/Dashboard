using Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dashboard.Models.Mappers
{
    public class DeliveryOrdersMapper
    {
        private IEnumerable<IMapper> mappers;

        public DeliveryOrdersMapper(IEnumerable<IMapper> mappers)
        {
            this.mappers = mappers;
        }

        public IEnumerable<T> Map<T>(IEnumerable<DeliveryOrder> orders)
        {
            if (typeof(T) == typeof(DeliveryOrder))
                return orders.Cast<T>();
            var mapper = mappers.FirstOrDefault(m => m.ToType == typeof(T));
            if (mapper == null)
                throw new ArgumentException(string.Format("No mapper has been defined for {0}", typeof(T).FullName));
            return mapper.Map(orders).Cast<T>();
        }
    }
}