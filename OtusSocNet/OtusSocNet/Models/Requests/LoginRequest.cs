using OtusSocNet.Models.Requests.Interfaces;
using System.ComponentModel.DataAnnotations;
using Utils.Extensions;

namespace OtusSocNet.Models.Requests;

public class LoginRequest : IValidatableRequest
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(20, MinimumLength = 8)]
    public string Password { get; set; }
    
    public bool IsValid()
    {
        return Id != default && !Password.IsNullOrWhiteSpace() && Password.Length is > 7 and <= 20;
    }
}