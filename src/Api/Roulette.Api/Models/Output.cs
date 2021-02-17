namespace Roulette.Api.Models
{
    internal abstract class Output
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
