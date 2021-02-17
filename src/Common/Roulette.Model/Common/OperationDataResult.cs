using System;
namespace Roulette.Model
{
    public class OperationDataResult<T> : OperationResult, ICloneable where T : class
    {
        public T Data { get; set; }
        public override object Clone()
        {
            var clone = (base.Clone() as OperationDataResult<T>);
            clone.Data = this.Data;
            return clone;
        }
    }
}
