using Microsoft.Extensions.Options;
using Roulette.Data;
using Roulette.Exceptions;
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
        private readonly IEnumerable<IRepository<Roulette.Model.Roulette>> _rouletteRepositories;
        private readonly IEnumerable<IRepository<Bet>> _betRepositories;
        public ScaleUpStrategy ScaleUpStrategy => ScaleUpStrategy.Monolithic;
        private readonly RouletteSettings _rouletteSettings;
        public EmbeddedInstanceService(IEnumerable<IRepository<Roulette.Model.Roulette>> rouletteRepositories,
                                       IOptions<RouletteSettings> rouletteSettingsOptions,
                                       IEnumerable<IRepository<Bet>> betRepositories)
        {
            _rouletteRepositories = rouletteRepositories;
            _rouletteSettings = rouletteSettingsOptions.Value;
            _betRepositories = betRepositories;
        }
        public async Task<OperationDataResult<IRoulette>> CreateNewRouletteAsync()
        {
            var roulette = await _rouletteRepositories.First(x => x.StorageProvider == _rouletteSettings.StorageProvider).CreateAsync((Roulette.Model.Roulette)(Activator.CreateInstance(typeof(Roulette.Model.Roulette))));
            return new OperationDataResult<IRoulette>
            {
                Success = true,
                Data = roulette
            };
        }
        public async Task<OperationResult> OpenRouletteAsync(string rouletteCode)
        {
            var result = new OperationResult();
            var rouletteStorage = _rouletteRepositories.First(x => x.StorageProvider == _rouletteSettings.StorageProvider);
            var roulette = await rouletteStorage.FindByCodeAsync(rouletteCode);
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
        public async Task<OperationResult> BetAsync(string rouletteCode, string userId, BetInput bet)
        {
            var result = new OperationResult();
            var rouletteStorage = _rouletteRepositories.First(x => x.StorageProvider == _rouletteSettings.StorageProvider);
            var roulette = await rouletteStorage.FindByCodeAsync(rouletteCode);
            if (roulette != null)
            {
                if (roulette.RouletteStatus != RouletteStatus.Open)
                {
                    throw new RouletteException("La ruleta se encuentra cerrada");
                }
                var betStorage = _betRepositories.First(x => x.StorageProvider == _rouletteSettings.StorageProvider);
                var betToSave = new Bet
                {
                    AmountToBet = bet.BetAmount,
                    RouletteCode = rouletteCode,
                    UserId = userId,
                    Number = bet.Number.ToString(),
                    Color = bet.Number % 2 == 0 ? Color.Red.ToString() : Color.Black.ToString()
                };
                await betStorage.CreateAsync(betToSave);
            }
            else
            {
                throw new RouletteException("No se encontró una ruleta abierta con ese código");
            }
            return result;
        }
        public async Task<OperationDataResult<CloseRouletteResult>> CloseRouletteAsync(string rouletteCode)
        {
            var result = new OperationDataResult<CloseRouletteResult> { Data = new CloseRouletteResult() };
            var rouletteStorage = _rouletteRepositories.First(x => x.StorageProvider == _rouletteSettings.StorageProvider);
            var roulette = await rouletteStorage.FindByCodeAsync(rouletteCode);
            if (roulette != null)
            {
                if (roulette.RouletteStatus != RouletteStatus.Open)
                {
                    throw new RouletteException("La ruleta ya se encuentra cerrada");
                }
                roulette.RouletteStatus = RouletteStatus.Closed;
                await rouletteStorage.UpdateAsync(roulette);
                result.Data.NumberWinner = new Random().Next(_rouletteSettings.MinNumberBet, _rouletteSettings.MaxNumberBet);
                result.Data.ColorWinner = new Random().Next(1, 3) == 1 ? Color.Black.ToString() : Color.Red.ToString();
                var betStorage = _betRepositories.First(x => x.StorageProvider == _rouletteSettings.StorageProvider);
                var betsNumberWinners = await betStorage.FindByStringsFiltersAsync(new Dictionary<string, string>
                {
                    { "RouletteCode",rouletteCode},
                    { "Number", result.Data.NumberWinner.ToString()}
                });
                var betsColorWinners = await betStorage.FindByStringsFiltersAsync(new Dictionary<string, string>
                {
                    { "RouletteCode",rouletteCode},
                    { "Color",result.Data.ColorWinner}
                });
                result.Data.Winners = GetWinnersAndgetMoney(betsNumberWinners, betsColorWinners);
            }
            else
            {
                result.Success = false;
            }
            return result;
        }
        private List<Winner> GetWinnersAndgetMoney(IEnumerable<Bet> numberWinners, IEnumerable<Bet> colorWinners)
        {
            var winners = new List<Winner>();
            foreach (var winner in numberWinners)
            {
                if (!winners.Any(x => x.UserId == winner.UserId))
                {
                    winners.Add(new Winner
                    {
                        UserId = winner.UserId,
                        IsNumberWinner = true,
                        TotalWon = winner.AmountToBet * 5
                    });
                }
            }
            foreach (var winner in colorWinners)
            {
                if (!winners.Any(x => x.UserId == winner.UserId))
                {
                    winners.Add(new Winner
                    {
                        UserId = winner.UserId,
                        IsNumberWinner = false,
                        TotalWon = winner.AmountToBet * 1.8
                    });
                }
            }
            return winners;
        }
        public async Task<OperationDataResult<IEnumerable<Roulette.Model.Roulette>>> GetRouletteListAsync()
        {
            return new OperationDataResult<IEnumerable<Roulette.Model.Roulette>>
            {
                Success = true,
                Data = await _rouletteRepositories.First(x => x.StorageProvider == _rouletteSettings.StorageProvider).FindByStringsFiltersAsync(null)
            };
        }
    }
}
