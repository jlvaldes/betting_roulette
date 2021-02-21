using Microsoft.Extensions.Options;
using Roulette.Model;
using Roulette.Services.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Roulette.Services
{
    public sealed class RouletteService : IRouletteService
    {
        private readonly IEnumerable<IInstanceService> _instanceServices;
        private readonly RouletteSettings _rouletteSettings;
        public RouletteService(IEnumerable<IInstanceService> instanceServices, IOptions<RouletteSettings> settingsOptions)
        {
            _instanceServices = instanceServices;
            _rouletteSettings = settingsOptions.Value;
        }
        public async Task<OperationDataResult<CreateRouletteResult>> CreateNewRouletteAsync()
        {
            var result = await _instanceServices.First(x => x.ScaleUpStrategy == _rouletteSettings.ScaleUpStrategy).CreateNewRouletteAsync();
            return new OperationDataResult<CreateRouletteResult>
            {
                Success = result.Success,
                Data = result.Success ? new CreateRouletteResult { RouletteCode = result.Data.Code} : null,
                Errors = result.Errors,
                Exception = result.Exception,
                Warnings = result.Warnings
            };
        }
        public async Task<OperationResult> OpenRouletteAsync(string rouletteCode)
        {
            return await _instanceServices.First(x => x.ScaleUpStrategy == _rouletteSettings.ScaleUpStrategy).OpenRouletteAsync(rouletteCode);
        }
        public async Task<OperationResult> BetAsync(string rouletteCode, string userId, BetInput body)
        {
            return await _instanceServices.First(x => x.ScaleUpStrategy == _rouletteSettings.ScaleUpStrategy).BetAsync(rouletteCode, userId, body);
        }
        public async Task<OperationDataResult<CloseRouletteResult>> CloseRouletteAsync(string rouletteCode)
        {
            return await _instanceServices.First(x => x.ScaleUpStrategy == _rouletteSettings.ScaleUpStrategy).CloseRouletteAsync(rouletteCode);
        }
        public async Task<OperationDataResult<IEnumerable<Roulette.Model.Roulette>>> GetRouletteListAsync()
        {
            return await _instanceServices.First(x => x.ScaleUpStrategy == _rouletteSettings.ScaleUpStrategy).GetRouletteListAsync();
        }
    }
}
