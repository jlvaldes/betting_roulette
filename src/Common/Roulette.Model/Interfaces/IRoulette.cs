namespace Roulette.Model
{
    public interface IRoulette : ICodificable
    {
        int WinningNumber { get; set; }
        Color WinningColor { get; set; }
        RouletteStatus RouletteStatus { get; set; }
        IRoulette OpenRoullete();
        IRoulette CloseRoullete();
    }
}
