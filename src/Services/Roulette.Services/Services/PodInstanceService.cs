using k8s;
using Roulette.Model;
using Roulette.Services.Model;
using System;
using System.Threading.Tasks;
namespace Roulette.Services.Services
{
    public sealed class PodInstanceService : IInstanceService
    {
        public ScaleUpStrategy ScaleUpStrategy => ScaleUpStrategy.Container;
        private readonly IKubernetes _kubernetesClient;
        public PodInstanceService(IKubernetes kubernetesClient)
        {
            _kubernetesClient = kubernetesClient ?? throw new ArgumentNullException(nameof(kubernetesClient));
        }
        public Task<OperationDataResult<IRoulette>> CreateNewRouletteAsync()
        {
            throw new NotImplementedException();
        }
        public Task<OperationResult> OpenRouletteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
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
    }
}
