using Microsoft.Extensions.Options;
using Roulette.Data;
using Roulette.Model;
using Roulette.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Roulette.Services.Services
{
    public sealed class EmbeddedInstanceService : IInstanceService
    {
        private readonly IEnumerable<IRepository<IRoulette>> _rouletteRepositories;
        private readonly IEnumerable<IRepository<IBet>> _betRepositories;
        public ScaleUpStrategy ScaleUpStrategy => ScaleUpStrategy.Embedded;
        private readonly RouletteSettings _rouletteSettings;
        public EmbeddedInstanceService(IEnumerable<IRepository<IRoulette>> rouletteRepositories,
                                       IOptions<RouletteSettings> rouletteSettingsOptions,
                                       IEnumerable<IRepository<IBet>> betRepositories)
        {
            _rouletteRepositories = rouletteRepositories;
            _rouletteSettings = rouletteSettingsOptions.Value;
            _betRepositories = betRepositories;
        }
        public async Task<OperationDataResult<IRoulette>> CreateNewRouletteAsync()
        {
            var roulette = await _rouletteRepositories.First(x => x.StorageProvider == _rouletteSettings.StorageProvider).CreateAsync((IRoulette)(Activator.CreateInstance(typeof(IRoulette))));
            return new OperationDataResult<IRoulette>
            {
                Success = true,
                Data = roulette
            };
        }
        public async Task<OperationResult> OpenRouletteAsync(Guid id)
        {
            var result = new OperationResult();
            var rouletteStorage = _rouletteRepositories.First(x => x.StorageProvider == _rouletteSettings.StorageProvider);
            var roulette = await rouletteStorage.FindByIdAsync(id);
            if (roulette != null)
            {
                roulette.RouletteStatus = RouletteStatus.Open;
                await rouletteStorage.UpdateAsync(roulette);
            }
            else
            {
                result.Success = false;
            }
            return result;
        }
        public async Task<OperationResult> BetAsync(Guid rouletteId, Guid userId, BetInput bet)
        {
            var result = new OperationResult();
            var rouletteStorage = _rouletteRepositories.First(x => x.StorageProvider == _rouletteSettings.StorageProvider);
            var roulette = await rouletteStorage.FindByIdAsync(rouletteId);
            if (roulette != null)
            {
                var betStorage = _betRepositories.First(x => x.StorageProvider == _rouletteSettings.StorageProvider);
                var betNumber = bet.Number != null ? (IBetNumber)(Activator.CreateInstance(typeof(IBetNumber))) : null;
                List<Task> tasks = new List<Task>();
                if (betNumber != null)
                {
                    betNumber.RouletteId = rouletteId;
                    betNumber.UserId = userId;
                    betNumber.AmountToBet = bet.BetAmount;
                    betNumber.Number = bet.Number.Value;
                    tasks.Add(betStorage.CreateAsync(betNumber));
                }
                var betColor = bet.Color != null ? (IBetColor)(Activator.CreateInstance(typeof(IBetColor))) : null;
                if (betColor != null)
                {
                    betColor.RouletteId = rouletteId;
                    betColor.UserId= userId;
                    betColor.AmountToBet = bet.BetAmount;
                    betColor.Color = bet.Color.Value;
                    tasks.Add(betStorage.CreateAsync(betColor));
                }
                await Task.WhenAll(tasks);
            }
            else
            {
                result.Success = false;
            }
            return result;
        }
    }
}
