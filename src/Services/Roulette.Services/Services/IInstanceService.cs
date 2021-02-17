using Roulette.Model;
using System.Threading.Tasks;
namespace Roulette.Services
{
    public interface IInstanceService
    {
        public ScaleUpStrategy ScaleUpStrategy { get; }
        Task<OperationDataResult<Model.Roulette>> CreateNewRouletteAsync();
    }
}
