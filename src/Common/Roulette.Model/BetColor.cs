using System;
using System.Threading.Tasks;
namespace Roulette.Model
{
    public sealed class BetColor : IBetColor
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid RouletteId { get; set; }
        public int AmountToBet { get; set; }
        public Color Color { get; set; }

    }
}
