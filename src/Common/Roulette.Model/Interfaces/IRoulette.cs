using System;
namespace Roulette.Model
{
    public interface IRoulette : IGuidable
    {
        int WinningNumber { get; set; }
        Color WinningColor { get; set; }
        RouletteStatus RouletteStatus { get; set; }
        IRoulette OpenRoullete();
        IRoulette CloseRoullete();
        void OpenBetColor(IBetColor betColor);
        void OpenBetNumber(IBetNumber betNumber);
    }
}
