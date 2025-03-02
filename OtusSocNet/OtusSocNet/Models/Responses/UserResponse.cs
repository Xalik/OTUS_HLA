namespace OtusSocNet.Models.Responses;

public class UserResponse
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public DateOnly BirthDate { get; set; }
    public bool IsMale { get; set; }
    public string Biography { get; set; }
    public string City { get; set; }
}