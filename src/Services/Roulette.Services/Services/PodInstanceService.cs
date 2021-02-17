using k8s;
using Roulette.Model;
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
        public Task<OperationDataResult<Model.Roulette>> CreateNewRouletteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
