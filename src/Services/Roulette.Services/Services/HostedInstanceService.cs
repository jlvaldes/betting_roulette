using Roulette.Model;
using Roulette.Services.Model;
using System;
using System.Threading.Tasks;
namespace Roulette.Services.Services
{
    public sealed class HostedInstanceService : IInstanceService
    {
        public ScaleUpStrategy ScaleUpStrategy => ScaleUpStrategy.Hosted;

        public Task<OperationResult> BetAsync(Guid id, int userId, BetInput body)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> BetAsync(Guid id, Guid userId, BetInput body)
        {
            throw new NotImplementedException();
        }

        public Task<OperationDataResult<CloseRouletteResult>> CloseRouletteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<OperationDataResult<IRoulette>> CreateNewRouletteAsync()
        {
            throw new System.NotImplementedException();
        }
        public Task<OperationResult> OpenRouletteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
