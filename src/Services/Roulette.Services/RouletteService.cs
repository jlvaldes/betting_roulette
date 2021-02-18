using Microsoft.Extensions.Options;
using Roulette.Model;
using Roulette.Services.Model;
using System;
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
                Data = result.Success ? new CreateRouletteResult { RouletteId = result.Data.Id} : null,
                Errors = result.Errors,
                Exception = result.Exception,
                Warnings = result.Warnings
            };
        }
        public async Task<OperationResult> OpenRouletteAsync(Guid id)
        {
            return await _instanceServices.First(x => x.ScaleUpStrategy == _rouletteSettings.ScaleUpStrategy).OpenRouletteAsync(id);
        }
        public async Task<OperationResult> BetAsync(Guid id, Guid userId, BetInput body)
        {
            return await _instanceServices.First(x => x.ScaleUpStrategy == _rouletteSettings.ScaleUpStrategy).BetAsync(id, userId, body);
        }
        public async Task<OperationDataResult<CloseRouletteResult>> CloseRouletteAsync(Guid id)
        {
            var result = new OperationDataResult<CloseRouletteResult>();
            var closeResult = await _instanceServices.First(x => x.ScaleUpStrategy == _rouletteSettings.ScaleUpStrategy).CloseRouletteAsync(id);
            return result;
        }
    }
}
