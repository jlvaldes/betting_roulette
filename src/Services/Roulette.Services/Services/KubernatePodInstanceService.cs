using k8s;
using Roulette.Model;
using Roulette.Services.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Roulette.Services.Services
{
    public sealed class KubernatePodInstanceService : IInstanceService
    {
        public ScaleUpStrategy ScaleUpStrategy => ScaleUpStrategy.Pipelines;
        private readonly IKubernetes _kubernetesClient;
        public KubernatePodInstanceService(IKubernetes kubernetesClient)
        {
            _kubernetesClient = kubernetesClient ?? throw new ArgumentNullException(nameof(kubernetesClient));
        }
        public Task<OperationDataResult<IRoulette>> CreateNewRouletteAsync()
        {
            throw new NotImplementedException();
        }
        public Task<OperationResult> OpenRouletteAsync(string rouletteCode)
        {
            throw new NotImplementedException();
        }
        public Task<OperationResult> BetAsync(string rouletteCode, int userId, BetInput body)
        {
            throw new NotImplementedException();
        }
        public Task<OperationResult> BetAsync(string rouletteCode, string userId, BetInput body)
        {
            throw new NotImplementedException();
        }
        public Task<OperationDataResult<CloseRouletteResult>> CloseRouletteAsync(string rouletteCode)
        {
            throw new NotImplementedException();
        }

        public Task<OperationDataResult<IEnumerable<Roulette.Model.Roulette>>> GetRouletteListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
