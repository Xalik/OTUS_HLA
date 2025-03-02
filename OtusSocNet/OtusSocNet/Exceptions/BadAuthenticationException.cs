using System.Net;

namespace OtusSocNet.Exceptions;

public class BadAuthenticationException : WebApiException
{
    public BadAuthenticationException() : base(HttpStatusCode.Forbidden, "Bad authentication") { }
}