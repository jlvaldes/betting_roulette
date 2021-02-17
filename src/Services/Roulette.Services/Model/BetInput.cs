using Roulette.Model;
namespace Roulette.Services.Model
{
    public class BetInput
    {
        public int BetAmount { get; set; }
        public int? Number { get; set; }
        public Color? Color { get; set; }
    }
}
