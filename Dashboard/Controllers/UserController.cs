using Dashboard.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dashboard.Service.Controllers
{
    // [Authorize]
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IDashboardUserRepository dashboardUserRepository;
        private readonly ILocationRepository locationRepository;

        public UserController(IDashboardUserRepository dashboardUserRepository, ILocationRepository locationRepository)
        {
            this.dashboardUserRepository = dashboardUserRepository;
            this.locationRepository = locationRepository;
        }

        [HttpGet("locations")]
        public async Task<IActionResult> GetLocations()
        {
            try
            {
                var locations = await locationRepository.GetAll();
                var userBranchIds = dashboardUserRepository.GetBranchIds(HttpContext.User);
                if (userBranchIds.Count() == 0)
                    return Ok(locations);
                var branches = locations.Where(l => userBranchIds.Contains(l.LocationId));
                return Ok(branches.Union(locations.Where(l => branches.Any(b => b.TradingAs == l.TradingAs && b.LocationId != l.LocationId))));
            }
            catch (InvalidCredentialException)
            {
                return Unauthorized();
            }
        }

        [HttpGet("viewstatistics")]
        public IActionResult CanViewStatistics()
        {
            return Ok(HttpContext.User.HasClaim(ClaimTypes.Role, "National User") || HttpContext.User.HasClaim(ClaimTypes.Role, "Regional User"));
        }
    }
}