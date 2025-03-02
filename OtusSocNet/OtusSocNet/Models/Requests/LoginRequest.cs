using OtusSocNet.Models.Requests.Interfaces;
using Utils.Extensions;

namespace OtusSocNet.Models.Requests;

public class LoginRequest : IValidatableRequest
{
    public string Id { get; set; }
    public string Password { get; set; }
    
    public bool IsValid()
    {
        return !Id.IsNullOrWhiteSpace() && !Password.IsNullOrWhiteSpace();
    }
}