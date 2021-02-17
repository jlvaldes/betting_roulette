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
        //[ProducesResponseType(typeof(OperationResult<BulkResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateRoulettaAsync()
        {
            return Ok();
        }
    }
}
