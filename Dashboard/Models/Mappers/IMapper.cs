using System;
using System.Collections.Generic;
using Dashboard.Models;

namespace Dashboard.Models.Mappers
{
    public interface IMapper
    {
        Type ToType { get; }
        IEnumerable<object> Map(IEnumerable<DeliveryOrder> orders);
    }
}