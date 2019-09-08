using Newtonsoft.Json;

namespace Dashboard.Models
{
    public class PackageNote
    {
        [JsonIgnore]
        public int PackageNoteId { get; set; }

        public string Packaging { get; set; }
        public string StagingArea { get; set; }
        public string Packer { get; set; }

        [JsonIgnore]
        public DeliveryOrder DeliveryOrder { get; set; }
    }
}
