namespace OtusSocNet.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string userId) : base($"User not found by id {userId}") { }
}