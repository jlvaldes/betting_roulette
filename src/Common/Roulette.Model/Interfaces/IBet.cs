using System;
namespace Roulette.Model
{
    public interface IBet
    {
        Guid Id { get; set; }
        Guid UserId { get; set; }
        Guid RouletteId { get; set; }
        int AmountToBet { get; set; }
    }
}
