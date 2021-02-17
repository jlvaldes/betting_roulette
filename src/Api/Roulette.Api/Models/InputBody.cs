namespace Roulette.Api.Models
{
    public abstract class Input<TBody> where TBody : class
    {
        public TBody Body { get; set; }
    }
}
