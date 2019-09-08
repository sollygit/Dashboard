using Dashboard.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dashboard.Service.Controllers
{
    // [Authorize]
    [Produces("application/json")]
    [Route("api/Source")]
    public class SourceController : Controller
    {
        private readonly ISourceRepository sourceRepository;

        public SourceController(ISourceRepository sourceRepository)
        {
            this.sourceRepository = sourceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await sourceRepository.GetAll());
        }
    }
}