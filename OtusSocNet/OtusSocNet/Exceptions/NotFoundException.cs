using System.Net;

namespace OtusSocNet.Exceptions;

public class NotFoundException : WebApiException
{
    public NotFoundException(string message) : base(HttpStatusCode.NotFound, message) { }
}