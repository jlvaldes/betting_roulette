namespace Roulette.Api.Middlewares
{
    public class HttpError
    {
        public bool Success { get; set; }
        public int HttpStatusCode { get; set; }
        public string Code { get; set; }
        public string[] Errors { get; set; }
        public string DeveloperMessage { get; set; }
        public string[] ValidationErrors { get; set; }
    }
}
