using System.Collections.Generic;
namespace Roulette.Services.Model
{
    public class CloseRouletteResult
    {
        public int NumberWinner { get; set; }
        public string ColorWinner { get; set; }
        public List<Winner> Winners { get; set; } = new List<Winner>();
    }
}
