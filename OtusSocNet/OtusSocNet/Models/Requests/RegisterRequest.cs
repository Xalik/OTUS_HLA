using OtusSocNet.Models.Requests.Interfaces;
using Utils.Extensions;

namespace OtusSocNet.Models.Requests;

public class RegisterRequest : IValidatableRequest
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public DateOnly BirthDate { get; set; }
    public bool IsMale { get; set; }
    public string Biography { get; set; }
    public string City { get; set; }
    public string Password { get; set; }
    
    public bool IsValid()
    {
        return !FirstName.IsNullOrWhiteSpace() && !SecondName.IsNullOrWhiteSpace() && !City.IsNullOrWhiteSpace() && !Password.IsNullOrWhiteSpace();
    }
}