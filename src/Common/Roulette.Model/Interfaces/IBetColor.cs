using System;
namespace Roulette.Model
{
    public interface IBetColor : IBet
    {
        Color Color { get; set; }
    }
}
