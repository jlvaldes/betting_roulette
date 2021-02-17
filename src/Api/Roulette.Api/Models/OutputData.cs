namespace Roulette.Api.Models
{
    internal class Output<TData> where TData : class
    {
        public TData Data { get; set; }
    }
}
