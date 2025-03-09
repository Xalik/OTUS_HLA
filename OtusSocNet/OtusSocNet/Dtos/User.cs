namespace OtusSocNet.Dtos;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public DateOnly BirthDate { get; set; }
    public bool IsMale { get; set; }
    public string Biography { get; set; }
    public string City { get; set; }
    public string Token { get; set; }
    public DateTime TokenExpires { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] Salt { get; set; }
}