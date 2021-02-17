using System;

namespace Roulette.Model
{
    public sealed class Roulette : IRoulette
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int WinningNumber { get; set; }
        public Color WinningColor { get; set; }
        public int BetColorCount { get; set; }
        public int BetNumberCount { get; set; }
        public RouletteStatus RouletteStatus { get; set; }
        public int TotalAmountBetColor { get; set; }
        public int TotalAmountBetNumber { get; set; }
        public IRoulette OpenRoullete()
        {
            return new Roulette();
        }
        public IRoulette CloseRoullete()
        {
            var ramdom = new Random();
            this.WinningNumber = ramdom.Next(0, 36);
            this.WinningColor = (Color)ramdom.Next(0, 1);
            this.RouletteStatus = RouletteStatus.Closed;
            return this;
        }
        public void OpenBetColor(IBetColor betColor)
        {
            this.BetColorCount++;
            this.TotalAmountBetColor += betColor.AmountToBet;
        }
        public void OpenBetNumber(IBetNumber betNumber)
        {
            this.BetNumberCount++;
            this.TotalAmountBetNumber += betNumber.AmountToBet;
        }
    }
}
