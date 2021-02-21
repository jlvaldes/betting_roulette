using System;
namespace Roulette.Model
{
    public class Bet : IBet
    {
        public string Code { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string RouletteCode { get; set; }
        public double AmountToBet { get; set; }
        public string Number { get; set; }
        public string Color { get; set; }
    }
}
