using System.Net;

namespace OtusSocNet.Exceptions;

public class BadRequestException : WebApiException
{
    public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message) { }

    public BadRequestException() : base(HttpStatusCode.BadRequest, "Incorrect request") { }
}