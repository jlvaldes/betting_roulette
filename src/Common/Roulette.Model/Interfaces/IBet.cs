namespace Roulette.Model
{
    public interface IBet : ICodificable
    {
        string UserId { get; set; }
        string RouletteCode { get; set; }
        double AmountToBet { get; set; }
        string Number { get; set; }
        string Color { get; set; }
    }
}
