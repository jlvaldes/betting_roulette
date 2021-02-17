using Roulette.Model;
using System.Threading.Tasks;
namespace Roulette.Services
{
    public interface IRouletteService
    {
        Task<OperationDataResult<Model.Roulette>> CreateNewRouletteAsync();
    }

}
