using System;
namespace Roulette.Exceptions
{
    public sealed class RouletteException : Exception
    {
        public RouletteExceptionCode ErrorCode { get; internal set; }
        public string MessageError { get; internal set; }
        public string UserMessage { get; internal set; }
        public RouletteException(string message = "", string userMessage = "") : base(message)
        {
            MessageError = !string.IsNullOrEmpty(message) ? message : i18nExceptions.RouletteException_General_Msg;
            UserMessage = !string.IsNullOrEmpty(userMessage) ? userMessage : i18nExceptions.RouletteException_User_Msg;
            ErrorCode = RouletteExceptionCode.GeneralErrorCode;
        }
    }
}
