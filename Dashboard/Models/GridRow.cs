using System;
using System.Collections.Generic;

namespace Dashboard.Models
{
    public class GridRow
    {
        public IEnumerable<string> Warnings { get; set; }
        public string DeliveryOrderId { get; set; }
        public int BranchId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerPromise { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryStatus { get; set; }
        public DateTimeOffset RequestDate { get; set; }
        public DateTimeOffset? PickupDateTime { get; set; }
        public DateTimeOffset? DeliveryDateTime { get; set; }
        public string TruckNumber { get; set; }
        public string FulfilmentType { get; set; }
        public string SourceId { get; set; }
        public string PickStatus { get; set; }
        public DateTimeOffset? PickStatusCompleteDateTime { get; set; }
        public Boolean OMUAppPacked { get; set; }
        public decimal Weight { get; set; }
        public List<Picker> Pickers { get; set; }
        public List<PackageNote> PackageNotes { get; set; }
        public Location Location { get; set; }        
    }
}