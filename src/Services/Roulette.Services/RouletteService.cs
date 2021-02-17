using Microsoft.Extensions.Options;
using Roulette.Model;
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
        public async Task<OperationDataResult<Model.Roulette>> CreateNewRouletteAsync()
        {
            return await _instanceServices.First(x => x.ScaleUpStrategy == _rouletteSettings.ScaleUpStrategy).CreateNewRouletteAsync();
        }
    }
}
