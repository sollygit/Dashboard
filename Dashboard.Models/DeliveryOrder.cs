using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dashboard.Models
{
    public class DeliveryOrder
    {
        public string DeliveryOrderId { get; set; }
        public string TransCode { get; set; }
        public int BranchId { get; set; }
        public Location Location { get; set; }
        public string CustomerId { get; set; }
        public DateTimeOffset RequestDate { get; set; }
        public DateTimeOffset? PickupDateTime { get; set; }
        public DateTimeOffset? DeliveryDateTime { get; set; }
        public string FulfilmentType { get; set; }
        public string SourceId { get; set; }
        public bool HoldReleaseFlag { get; set; }
        public string CustomerPromise { get; set; }
        public string PickStatus { get; set; }
        public DateTimeOffset? PickStatusCompleteDateTime { get; set; }
        public bool OMUAppPacked { get; set; }
        public string PickArea { get; set; }
        public decimal Weight { get; set; }
        public string SpareField { get; set; }
        public List<Picker> Pickers { get; set; }
        public List<PackageNote> PackageNotes { get; set; }
        public List<Line> Lines { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryStatus { get; set; }
        public string TruckNumber { get; set; }
        [JsonIgnore]
        public DateTimeOffset LastUpdated { get; set; }
    }
}