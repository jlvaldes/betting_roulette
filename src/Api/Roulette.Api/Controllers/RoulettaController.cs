using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Roulette.Api.Controllers
{
    [Route("api/[controller]")]
    public class RoulettaController : ControllerBase
    {
        /// <summary>
        /// Creating a wheel of fortune
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateRoulettaAsync()
        {
            return Ok();
        }
    }
}
