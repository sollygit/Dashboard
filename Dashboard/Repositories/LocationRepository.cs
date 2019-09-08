using Dashboard.Interfaces;
using Dashboard.Models;
using Dashboard.Settings;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dashboard.Extensions
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DashboardContext context;
        private readonly BranchFinderSettings settings;
        private IEnumerable<Location> cachedLocations;
        private DateTime lastCacheUpdate;

        public LocationRepository(DashboardContext context, BranchFinderSettings settings)
        {
            this.context = context;
            this.settings = settings;
        }

        public async Task<IEnumerable<Location>> GetAll()
        {
            return await RetrieveLocations();
        }

        public async Task<Location> Get(int locationId)
        {
            var location = await context.Locations.FindAsync(locationId);
            if (location == null)
                location = (await RetrieveLocations()).SingleOrDefault(l => l.LocationId == locationId);
            return location;
        }

        public async Task<IEnumerable<Location>> GetBranches()
        {
            return (await RetrieveLocations()).Where(l => !l.IsDepot);
        }

        private async Task<IEnumerable<Location>> RetrieveLocations()
        {
            if (cachedLocations == null || lastCacheUpdate <= DateTime.Now.AddHours(-1 * settings.CachePeriod))
            {
                using (var client = new HttpClient())
                {
                    var uriBuilder = new UriBuilder(settings.Url)
                    {
                        Query = string.Format("TransactionId={0}", Guid.NewGuid().ToString().Replace("-", ""))
                    };
                    var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);
                    request.Headers.Add("Ocp-Apim-Subscription-Key", settings.SubscriptionKey);
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var content = JObject.Parse(await response.Content.ReadAsStringAsync());
                    var textInfo = CultureInfo.CurrentCulture.TextInfo;
                    cachedLocations = ((JArray)content["Branches"]).Select(b => {
                        return new Location
                        {
                            LocationId = b["LocationId"].Value<int>(),
                            Name = textInfo.ToTitleCase(b["Name"].Value<string>().Substring(12)),
                            TradingAs = b["TradingAs"].Value<string>(),
                            IsDepot = b["IsDepot"].Value<string>() == "Yes"
                        };
                    });
                    lastCacheUpdate = DateTime.Now;
                }
            }
            return cachedLocations;
        }
    }
}
