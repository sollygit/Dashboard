using Dashboard.Interfaces;
using Dashboard.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dashboard.Extensions
{
    public class InMemorySourceRepository : ISourceRepository
    {
        private readonly List<Source> sources;

        public InMemorySourceRepository()
        {
            sources = new List<Source>()
            {
                new Source { Code = "Bath", Description = "Bathroom Department" },
                new Source { Code = "Bldt", Description = "BuildIT" },
                new Source { Code = "Call", Description = "Call in sales" },
                new Source { Code = "Crrc", Description = "Recharge" },
                new Source { Code = "Cust", Description = "Customer collection" },
                new Source { Code = "Delv", Description = "Delivery" },
                new Source { Code = "Dirs", Description = "Direct Ship" },
                new Source { Code = "ELIX", Description = "Elinx Orders" },
                new Source { Code = "Faxd", Description = "Faxed order" },
                new Source { Code = "FMSH", Description = "Farm Sheds" },
                new Source { Code = "Home", Description = "HomeStar Enquiry" },
                new Source { Code = "Ibch", Description = "Interbranch Sales" },
                new Source { Code = "Inst", Description = "Install Solution" },
                new Source { Code = "KTCH", Description = "Kitchens" },
                new Source { Code = "Manu", Description = "Manufacturing" },
                new Source { Code = "Phnd", Description = "Telephone order" },
                new Source { Code = "pink", Description = "Pink Batts Fit" },
                new Source { Code = "Plus", Description = "Plus Points Sale" },
                new Source { Code = "Preh", Description = "Prehung Doors" },
                new Source { Code = "Prom", Description = "Promo evening" },
                new Source { Code = "QPL", Description = "Quick Pick Lane" },
                new Source { Code = "Rain", Description = "Raincheck" },
                new Source { Code = "Remo", Description = "Remote Order Number" },
                new Source { Code = "Rep", Description = "Rep order" },
                new Source { Code = "Roof", Description = "Roofing" },
                new Source { Code = "SMRT", Description = "Smart Start" },
                new Source { Code = "SOS", Description = "Short Order Service" },
                new Source { Code = "Spor", Description = "Special Order" },
                new Source { Code = "TFER", Description = "Transfer" },
                new Source { Code = "VAN1", Description = "Sales from Van1" },
                new Source { Code = "VAN2", Description = "Sales from Van2" }
            };
        }

        public async Task<IEnumerable<Source>> GetAll()
        {
            await Task.Delay(1);
            return sources;
        }
    }
}
