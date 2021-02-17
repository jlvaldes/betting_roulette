namespace Roulette.Api.Models
{
    public class Input<TBody> : Input where TBody : class
    {
        public TBody Body { get; set; }
    }
}
