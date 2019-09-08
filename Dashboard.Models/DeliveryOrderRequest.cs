using System;

namespace Dashboard.Models
{
    public class DeliveryOrderRequest
    {
        public string TransactionId { get; set; }
        public DeliveryOrder DeliveryOrder { get; set; }
    }
}