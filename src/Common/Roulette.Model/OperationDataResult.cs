namespace Roulette.Model
{
    public class OperationDataResult<T> : OperationResult where T : class
    {
        public T Data { get; set; }
    }
}
