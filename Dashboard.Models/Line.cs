using System;

namespace Dashboard.Models
{
    public class Line
    {
        public int LineId { get; set; }
        public int LineNumber { get; set; }
        public string Sku { get; set; }
        public bool SpecialOrder { get; set; }
        public bool Substitution { get; set; }
        public bool BackOrder { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public decimal Quantity { get; set; }
        public decimal StockOnHand { get; set; }
        public decimal QuantityPicked { get; set; }
        public string Picker { get; set; }

        public DeliveryOrder DeliveryOrder { get; set; }
    }
}