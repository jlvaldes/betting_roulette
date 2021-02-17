using Roulette.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
namespace Roulette.Model
{
    public class OperationResult : ICloneable
    {
        public List<string> Errors { get; set; }
        public List<string> Warnings { get; set; }
        public bool Success { get; set; } = true;
        public string Code
        {
            get
            {
                return Exception != null && Exception is RouletteException exception ? ((int)exception.ErrorCode).ToString() : string.Empty;
            }
        }
        [JsonIgnore]
        public Exception Exception { get; set; }
        public OperationResult()
        {
            Errors = new List<string>();
            Warnings = new List<string>();
        }
        public bool HasErrors
        {
            get
            {
                return Errors.Any();
            }
        }
        public bool HasWarnings
        {
            get
            {
                return Warnings.Any();
            }
        }
        public string JoinErrorMessage()
        {
            return HasErrors ? string.Join(" ", Errors) : string.Empty;
        }
        public virtual object Clone()
        {
            return new OperationResult
            {
                Success = this.Success,
                Errors = this.Errors,
                Exception = this.Exception,
                Warnings = this.Warnings
            };
        }
    }
}
