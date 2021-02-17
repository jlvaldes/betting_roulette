using Roulette.Model;
using System.Threading.Tasks;
namespace Roulette.Services.Services
{
    public sealed class HostedInstanceService : IInstanceService
    {
        public ScaleUpStrategy ScaleUpStrategy => ScaleUpStrategy.Hosted;

        public Task<OperationDataResult<Model.Roulette>> CreateNewRouletteAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
