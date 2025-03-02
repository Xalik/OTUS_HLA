using System.Net;

namespace OtusSocNet.Exceptions;

public class WebApiException : Exception
{
    public HttpStatusCode StatusCode { get; private set; }

    public WebApiException(HttpStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }

    public WebApiException(string message) : base(message)
    {
        StatusCode = HttpStatusCode.InternalServerError;
    }
}