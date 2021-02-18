using System;
namespace Roulette.Model
{
    public interface IBet : IGuidable
    {
        Guid UserId { get; set; }
        Guid RouletteId { get; set; }
        int AmountToBet { get; set; }
    }
}
