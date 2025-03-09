using OtusSocNet.Models.Requests.Interfaces;
using System.ComponentModel.DataAnnotations;
using Utils.Extensions;

namespace OtusSocNet.Models.Requests;

public class RegisterRequest : IValidatableRequest
{
    [Required]
    [StringLength(20, MinimumLength = 1)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(20, MinimumLength = 1)]
    public string SecondName { get; set; }
    
    [Required]
    public DateOnly BirthDate { get; set; }
    
    [Required]
    public bool IsMale { get; set; }
    
    [StringLength(100, MinimumLength = 0)]
    public string Biography { get; set; }
    
    [Required]
    [StringLength(20, MinimumLength = 1)]
    public string City { get; set; }
    
    [Required]
    [StringLength(20, MinimumLength = 8)]
    public string Password { get; set; }
    
    public bool IsValid()
    {
        return !FirstName.IsNullOrWhiteSpace() && FirstName.Length is > 0 and <= 20 && 
               !SecondName.IsNullOrWhiteSpace() && SecondName.Length is > 0 and <= 20 &&
               (Biography.IsNullOrWhiteSpace() || Biography.Length is > 0 and <= 100) &&
               !City.IsNullOrWhiteSpace() && City.Length is > 0 and <= 20 &&
               !Password.IsNullOrWhiteSpace() && Password.Length is > 7 and <= 20;
    }
}