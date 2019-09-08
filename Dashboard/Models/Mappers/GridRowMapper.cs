using Dashboard.Models.Warnings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dashboard.Models.Mappers
{
    public class GridRowMapper : IMapper
    {
        public Type ToType => typeof(GridRow);

        private readonly WarningTester warningTester;
        private readonly Dictionary<PropertyInfo, PropertyInfo> mapping;

        public GridRowMapper(WarningTester warningTester)
        {
            this.warningTester = warningTester;
            mapping = new Dictionary<PropertyInfo, PropertyInfo>();
            var deliveryOrderProperties = typeof(DeliveryOrder).GetProperties();
            var properties = typeof(GridRow).GetProperties();
            foreach (var property in properties)
            {
                var deliveryOrderProperty = deliveryOrderProperties.FirstOrDefault(p => p.Name == property.Name);
                if (deliveryOrderProperty != null)
                    mapping.Add(deliveryOrderProperty, property);
            }
        }

        public IEnumerable<object> Map(IEnumerable<DeliveryOrder> orders)
        {
            return orders.Select(o => Map(o));
        }

        private GridRow Map(DeliveryOrder order)
        {
            var item = new GridRow();
            foreach (var map in mapping)
            {
                var value = map.Key.GetValue(order);
                if (value != null)
                    map.Value.SetValue(item, value);
            }
            item.Warnings = warningTester.Test(order);
            return item;
        }
    }
}