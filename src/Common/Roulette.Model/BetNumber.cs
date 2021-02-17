using System;
namespace Roulette.Model
{
    public sealed class BetNumber : IBetNumber
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid RouletteId { get; set; }
        public int AmountToBet { get; set; }
        public int Number { get; set; }
    }
}
