namespace OtusSocNet.Dtos;

public class RegisterParameters
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public DateOnly BirthDate { get; set; }
    public bool IsMale { get; set; }
    public string Biography { get; set; }
    public string City { get; set; }
    public string Password { get; set; }
}