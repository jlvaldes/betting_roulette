using System;

namespace Roulette.Model
{
    public sealed class Roulette : IRoulette
    {
        public string Code { get; set; } = Guid.NewGuid().ToString();
        public int WinningNumber { get; set; }
        public Color WinningColor { get; set; }
        public RouletteStatus RouletteStatus { get; set; }
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
    }
}
