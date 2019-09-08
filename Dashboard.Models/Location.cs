using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public bool IsDepot { get; set; }
        public string TradingAs { get; set; }

        [JsonIgnore]
        public List<DeliveryOrder> DeliveryOrders { get; set; }
    }
}
