using System.Collections.Generic;
namespace Roulette.Model
{
    public sealed class RouletteSettings
    {
        public List<string> Cors { get; set; }
        public ScaleUpStrategy ScaleUpStrategy { get; set; }
        public StorageProvider StorageProvider { get; set; }
    }
}
