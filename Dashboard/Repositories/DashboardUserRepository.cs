using Dashboard.Interfaces;
using Dashboard.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Dashboard.Extensions
{
    public class DashboardUserRepository : IDashboardUserRepository
    {
        private AdminApiSettings adminApiSettings;

        public DashboardUserRepository(AdminApiSettings adminApiSettings)
        {
            this.adminApiSettings = adminApiSettings;
        }

        public IEnumerable<int> GetBranchIds(ClaimsPrincipal principal)
        {
            if (principal.HasClaim(ClaimTypes.Role, "National User"))
                return new int[] { };
            var branchProperties = principal.FindAll(ClaimTypes.Role).SelectMany(c => c.Properties);
            var branches = branchProperties.Where(p => p.Key.StartsWith("TMS.Administration.Branch")).Select(c => int.Parse(c.Value)).Distinct();
            //if (branches.Count() == 0)
            //    throw new InvalidCredentialException();
            return branches;
        }
        
        private class ClaimConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType) => objectType == typeof(Claim);

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var claimObject = JObject.Load(reader);
                var claim = new Claim(
                    claimObject["type"].Value<string>(),
                    claimObject["value"].Value<string>(),
                    claimObject["valueType"].Value<string>(),
                    claimObject["issuer"].Value<string>(),
                    claimObject["originalIssuer"].Value<string>());
                foreach (var property in (JObject)claimObject["properties"])
                {
                    claim.Properties.Add(property.Key, property.Value.Value<string>());
                }
                return claim;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
