using Dashboard.Models;
using Dashboard.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Dashboard.Service.Controllers
{
    // [Authorize]
    [Produces("application/json")]
    [Route("api/statistics")]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly IDashboardUserRepository _dashboardUserRepository;

        public StatisticsController(IStatisticsRepository statisticsRepository, IDashboardUserRepository dashboardUserRepository)
        {
            _statisticsRepository = statisticsRepository;
            _dashboardUserRepository = dashboardUserRepository;
        }

        [HttpGet("{lastUpdate?}")]
        public async Task<IActionResult> Get(DateTimeOffset? lastUpdate)
        {
            try
            {
                int[] branchIds = _dashboardUserRepository.GetBranchIds(User)?.ToArray();
                var orders = await (lastUpdate.HasValue ? _statisticsRepository.GetSince<GridRow>(lastUpdate.Value, branchIds) : _statisticsRepository.Get<GridRow>(branchIds));
                return Ok(orders);
            }
            catch (InvalidCredentialException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }            
        }

        [HttpGet("national")]
        public async Task<IActionResult> National()
        {
            try
            {
                return Ok(await _statisticsRepository.Get<NationalOrderStatus>());
            }
            catch (InvalidCredentialException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }        
    }
}