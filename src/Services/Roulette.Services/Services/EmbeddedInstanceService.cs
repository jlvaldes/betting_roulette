using Roulette.Model;
using System.Threading.Tasks;
namespace Roulette.Services.Services
{
    public sealed class EmbeddedInstanceService : IInstanceService
    {
        public ScaleUpStrategy ScaleUpStrategy => ScaleUpStrategy.Embedded;
        public Task<OperationDataResult<Model.Roulette>> CreateNewRouletteAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
