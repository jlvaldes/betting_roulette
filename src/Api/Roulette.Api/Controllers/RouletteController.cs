using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roulette.Api.Models;
using Roulette.Model;
using Roulette.Services;
using Roulette.Services.Model;
using System;
using System.Threading.Tasks;
namespace Roulette.Api.Controllers
{
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
        /// <returns>Returns the ID of the created betting roulette</returns>
        [HttpPost]
        [ProducesResponseType(typeof(OperationDataResult<CreateRouletteResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateRoulettaAsync()
        {
            return Ok(await _rouletteService.CreateNewRouletteAsync());
        }
        /// <summary>
        /// Opening a betting roulette
        /// </summary>
        /// <param name="id">Roulette ID</param>
        /// <returns>Returns the status of the roulette opening operation</returns>
        [Route("open/{id}")]
        [HttpPost]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> OpenRoulettaAsync(Guid id)
        {
            return Ok(await _rouletteService.OpenRouletteAsync(id));
        }
        /// <summary>
        /// To bet on a number and/or a color on a roulette wheel
        /// </summary>
        /// <param name="id">Roulette ID</param>
        /// <param name="input">Request body</param>
        /// <returns>Returns a model with the status of the execution of the bet</returns>
        [Route("bet/{id}")]
        [HttpPost]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> BetRoulettaAsync(Guid id, Input<BetInput> input)
        {
            return Ok(await _rouletteService.BetAsync(id, input.UserId, input.Body));
        }
        /// <summary>
        /// Closing a betting roulette
        /// </summary>
        /// <param name="id">Roulette ID</param>
        /// <returns></returns>
        [Route("close/{id}")]
        [HttpPost]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CloseRoulettaAsync(Guid id)
        {
            return Ok(await _rouletteService.CloseRouletteAsync(id));
        }
    }
}
