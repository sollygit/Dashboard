using System;

namespace Dashboard.Models
{
    public class Picker
    {
        public int PickerId { get; set; }
        public string Name { get; set; }
        public DeliveryOrder DeliveryOrder { get; set; }
    }
}