using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roulette.Model;
using Roulette.Services;
using Roulette.Services.Model;
using System;
using System.Threading.Tasks;
namespace Roulette.Api.Controllers
{
    [ApiController]
    [Route("api/roulette")]
    public class RouletteController : ControllerBase
    {
        private readonly IRouletteService _rouletteService;
        public RouletteController(IRouletteService rouletteService)
        {
            _rouletteService = rouletteService;
        }
        /// <summary>
        /// Creating a wheel of fortune
        /// </summary>
        /// <returns>Returns the code of the created betting roulette</returns>
        [HttpPost]
        [ProducesResponseType(typeof(OperationDataResult<CreateRouletteResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateRoulettaAsync()
        {
            return Ok(await _rouletteService.CreateNewRouletteAsync());
        }
        /// <summary>
        /// Get roulette list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(OperationDataResult<>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRouletteListAsync()
        {
            return Ok(await _rouletteService.GetRouletteListAsync());
        }
        /// <summary>
        /// Opening a betting roulette
        /// </summary>
        /// <param name="rouletteCode">Roulette code</param>
        /// <returns>Returns the status of the roulette opening operation</returns>
        [Route("open/{rouletteCode}")]
        [HttpPost]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> OpenRoulettaAsync(string rouletteCode)
        {
            return Ok(await _rouletteService.OpenRouletteAsync(rouletteCode));
        }
        /// <summary>
        /// To bet on a number and/or a color on a roulette wheel
        /// </summary>
        /// <param name="rouletteCode">Roulette code</param>
        /// <param name="input">Request body</param>
        /// <param name="userId">User ID</param>
        /// <returns>Returns a model with the status of the execution of the bet</returns>
        [Route("bet/{rouletteCode}")]
        [HttpPost]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> BetRoulettaAsync(string rouletteCode, BetInput input, [FromHeader(Name = "User-Id")] string userId)
        {
            return Ok(await _rouletteService.BetAsync(rouletteCode, userId, input));
        }
        /// <summary>
        /// Closing a betting roulette
        /// </summary>
        /// <param name="rouletteCode">Roulette ID</param>
        /// <returns></returns>
        [Route("close/{rouletteCode}")]
        [HttpPost]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CloseRoulettaAsync(string rouletteCode)
        {
            return Ok(await _rouletteService.CloseRouletteAsync(rouletteCode));
        }
    }
}
