using System;

namespace Dashboard.Models
{
    public class DeliveryOrderResponse
    {
        public string TransactionId { get; set; }
        public ServiceResult ServiceResult { get; set; }
    }
}